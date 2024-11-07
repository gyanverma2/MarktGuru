using System.Net;

namespace MarktGuru.Products.Common.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string? Details { get; }

        public ApiException(HttpStatusCode statusCode, string message, string? details = null) : base(message)
        {
            StatusCode = statusCode;
            Details = details;
        }
    }
}
