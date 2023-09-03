using Core.Api.Domain.Exceptions;
using Core.Api.Domain.Handlers;
using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Api.Banking.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BankingException e)
            {
                _logger.LogError(e, "Se ha encontrado una excepción de tipo banking exception");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var response = new
                {
                    errorCode = e.ErrorCode,
                    errorMessage = e.Message,
                }; 

                var respondeJSON = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(respondeJSON);    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha encontrado una excepción de tipo exception");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new
                {
                    errorCode = "ERROR_DESCONOCIDO",
                    errorMessage = ex.Message,
                };

                var respondeJSON = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(respondeJSON);
            }
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExeptionHandler(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
