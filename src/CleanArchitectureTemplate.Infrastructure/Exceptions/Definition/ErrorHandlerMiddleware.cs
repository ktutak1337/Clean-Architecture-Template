using System;
using System.Threading.Tasks;
using ApplicationException = CleanArchitectureTemplate.Application.Exceptions.ApplicationException;
using CleanArchitectureTemplate.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureTemplate.Infrastructure.Exceptions.Definition
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<ErrorHandlerMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await HandleErrorAsync(context, exception);
            }
        }

        private async static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var code = "Error";
            var message = exception.Message;
            var statusCode = 400;

            (code, message) = exception switch
            {
                InfrastructureException ex => (ex.Code, ex.Message),
                ApplicationException ex => (ex.Code, ex.Message),
                DomainException ex => (ex.Code, ex.Message),
                _ => ("Error", "There was an error.")
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new {code, message});
        }
    }
}
