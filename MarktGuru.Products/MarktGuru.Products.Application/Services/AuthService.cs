using MarktGuru.Products.Common.Exceptions;
using MarktGuru.Products.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MarktGuru.Products.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration, ILogger<AuthService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public TokenResponse Authenticate(string username, string password)
        {
            _logger.LogInformation("AuthService: Authenticating user {Username}", username);
            var authSettings = _configuration.GetSection("Authentication");
            if (username != authSettings["Username"] || password != authSettings["Password"])
            {
                _logger.LogWarning("Authentication failed for user {Username}", username);
                throw new AuthenticationException("Invalid username or password");
            }

            var token = GenerateJwtToken(username,out DateTime expiration);
            return new TokenResponse
            {
                AccessToken = token,
                Expiration = expiration
            };
        }
        private string GenerateJwtToken(string username,out DateTime expiration)
        {
            _logger.LogInformation("AuthService: Generating JWT token");
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? string.Empty);
            expiration = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationMinutes"] ?? "10"));
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, expiration.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
            );

            _logger.LogInformation("AuthService: JWT token generated successfully");
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
