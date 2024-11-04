using MarktGuru.Products.Common.Exceptions;
using Xunit;

namespace MarketGuru.Products.Common.Test.Exceptions
{
    public class AuthenticationExceptionTest
    {
        private const string DefaultMessage = "Authentication failed";
        [Fact]
        public void Constructor_WithMessage_SetsMessage()
        {
            var exception = new AuthenticationException(DefaultMessage);

            Assert.Equal(DefaultMessage, exception.Message);
        }
    }
}
