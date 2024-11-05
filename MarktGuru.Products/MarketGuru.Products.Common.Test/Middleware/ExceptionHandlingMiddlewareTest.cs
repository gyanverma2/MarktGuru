using MarktGuru.Products.Common.Exceptions;
using MarktGuru.Products.Common.Middleware;
using Microsoft.AspNetCore.Http;
using System.Net;
using Xunit;

namespace MarketGuru.Products.Common.Test.Middleware
{
    public class ExceptionHandlingMiddlewareTest
    {
        [Fact]
        public async Task InvokeAsync_WithApiException_ReturnsApiExceptionAsync()
        {
            var context = new DefaultHttpContext();
            var middleware = new ExceptionHandlingMiddleware((innerHttpContext) => throw new ApiException(HttpStatusCode.BadRequest, "Bad request"));
            await middleware.InvokeAsync(context);
            Assert.Equal((int)HttpStatusCode.BadRequest, context.Response.StatusCode);
        }
        [Fact]
        public async Task InvokeAsync_WithAuthenticationException_ReturnsAuthenticationExceptionAsync()
        {
            var context = new DefaultHttpContext();
            var middleware = new ExceptionHandlingMiddleware((innerHttpContext) => throw new AuthenticationException("Authentication failed"));
            await middleware.InvokeAsync(context);
            Assert.Equal((int)HttpStatusCode.Unauthorized, context.Response.StatusCode);
        }
        [Fact]
        public async Task InvokeAsync_WithException_ReturnsExceptionAsync()
        {
            var context = new DefaultHttpContext();
            var middleware = new ExceptionHandlingMiddleware((innerHttpContext) => throw new Exception("An error occurred."));
            await middleware.InvokeAsync(context);
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        }
    }
}
