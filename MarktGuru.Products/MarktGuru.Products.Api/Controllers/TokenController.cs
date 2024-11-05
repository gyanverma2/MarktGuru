using MarktGuru.Products.Api.Constants;
using MarktGuru.Products.Application.Services;
using MarktGuru.Products.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarktGuru.Products.Api.Controllers
{
    [Route(ApiRoutes.BaseV1)]
    [ApiController]
    public class TokenController(IAuthService authService,ILogger<TokenController> logger) : ControllerBase
    {
        private readonly ILogger<TokenController> _logger = logger;
        private readonly IAuthService _authService = authService;

        /// <summary>
        /// Authenticates a user and returns a token
        /// </summary>
        /// <param name="request">AuthenticateRequest</param>
        /// <returns>TokenResponse</returns>
        /// <response>Return authentication token</response>
        [HttpPost]
        [Route("authenticate")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            _logger.LogInformation("Authenticating user {Username}", request.Username);

            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                _logger.LogWarning("Invalid request");
                return BadRequest();
            }

            var token = _authService.Authenticate(request.Username, request.Password);

            _logger.LogInformation("User {Username} authenticated successfully", request.Username);
            return Ok(token);
        }
    }
}
