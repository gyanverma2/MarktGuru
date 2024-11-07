using MarktGuru.Products.Common.Exceptions;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MarktGuru.Products.Common.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            int statusCode;

            switch (ex)
            {
                case RecordNotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ApiException apiEx:
                    statusCode = (int)apiEx.StatusCode;
                    break;
                case ValidationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case AuthenticationException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new
            {
                error = new
                {
                    context.Response.StatusCode,
                    details = ex.Message
                }
            });

            return context.Response.WriteAsync(result);
        }
    }
}
