using MarktGuru.Products.Common.Exceptions;
using System.Net;
using Xunit;

namespace MarketGuru.Products.Common.Test.Exceptions
{
    public class ApiExceptionTest
    {
        private const string DefaultMessage = "An error occurred.";
        private const string DefaultDetails = "Details";
        private const HttpStatusCode DefaultStatusCode = HttpStatusCode.InternalServerError;
        [Fact]
        public void Constructor_WithStatusCodeAndMessage_SetsStatusCodeAndMessage()
        {
            var exception = new ApiException(DefaultStatusCode, DefaultMessage);

            Assert.Equal(DefaultStatusCode, exception.StatusCode);
            Assert.Equal(DefaultMessage, exception.Message);
        }
        [Fact]
        public void Constructor_WithStatusCodeMessageAndDetails_SetsStatusCodeMessageAndDetails()
        {
            var exception = new ApiException(DefaultStatusCode, DefaultMessage,DefaultDetails);

            Assert.Equal(DefaultStatusCode, exception.StatusCode);
            Assert.Equal(DefaultMessage, exception.Message);
            Assert.Equal(DefaultDetails, exception.Details);
        }
        [Fact]
        public void Constructor_WithStatusCodeMessageAndNullDetails_SetsStatusCodeMessageAndNullDetails()
        {
            var exception = new ApiException(DefaultStatusCode, DefaultMessage);

            Assert.Equal(DefaultStatusCode, exception.StatusCode);
            Assert.Equal(DefaultMessage, exception.Message);
            Assert.Null(exception.Details);
        }
        [Fact]
        public void Constructor_WithStatusCodeMessageAndEmptyDetails_SetsStatusCodeMessageAndEmptyDetails()
        {
            var exception = new ApiException(DefaultStatusCode, DefaultMessage,string.Empty);

            Assert.Equal(DefaultStatusCode, exception.StatusCode);
            Assert.Equal(DefaultMessage, exception.Message);
            Assert.Equal(string.Empty,exception.Details);
        }
    }
}
