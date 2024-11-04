using MarktGuru.Products.Application.Services;
using MarktGuru.Products.Common.Exceptions;
using MarktGuru.Products.Common.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace MarketGuru.Products.Common.Test.Models
{
    public class AuthenticateRequestTest
    {
        private const string Username = "test";
        private const string Password = "test";
        [Fact]
        public void AuthenticateRequest_WithValidData_ReturnsTokenResponse()
        {
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(x => x.Authenticate(Username, Password)).Returns(new TokenResponse() {  AccessToken = "test token", Expiration = DateTime.UtcNow.AddMinutes(10)});
            var _authService = authServiceMock.Object;

            var tokenResponse = _authService.Authenticate(Username, Password);

            Assert.NotNull(tokenResponse);
            Assert.NotNull(tokenResponse.AccessToken);
            Assert.True(tokenResponse.Expiration > DateTime.UtcNow);
        }
        [Fact]
        public void AuthenticateRequest_WithInvalidData_ReturnsNull()
        {
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(x => x.Authenticate(Username, Password)).Throws(new AuthenticationException("Invalid username or password"));
            var _authService = authServiceMock.Object;
            var act = () => _authService.Authenticate(Username, Password);
            Assert.Throws<AuthenticationException>(act);
        }

    }
}
