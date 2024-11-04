using MarktGuru.Products.Common.Models;
using Xunit;

namespace MarketGuru.Products.Common.Test.Models
{
    public class TokenResponseTest
    {
        private const string AccessToken = "access_token";
        private readonly DateTime Expiration = DateTime.Now;
        [Fact]
        public void Constructor_Test()
        {
            var token = new TokenResponse()
            {
                AccessToken = AccessToken,
                Expiration = Expiration
            };
            Assert.Equal(AccessToken, token.AccessToken);
            Assert.Equivalent(Expiration, token.Expiration);
        }
    }
}
