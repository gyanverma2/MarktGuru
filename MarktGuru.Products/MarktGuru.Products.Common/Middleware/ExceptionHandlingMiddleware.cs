using MarktGuru.Products.Common.Exceptions;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

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
            catch(ApiException apiEx)
            {
                context.Response.StatusCode = (int)apiEx.StatusCode;
                await HandleExceptionAsync(context, apiEx);
            }
            catch(AuthenticationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await HandleExceptionAsync(context, ex);
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
