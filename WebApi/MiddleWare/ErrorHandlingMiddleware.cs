using System.Net;
using System.Text.Json;
using Serilog;

namespace BookStoreApi.WebApi.MiddleWare
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unhandled exception occured");

                await HandleExceptionAsync(context, ex);
            }
        }


        public Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                message = "An unexpeted error occured.",
                error = ex.Message,
                traceId = context.TraceIdentifier
            };

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
