using System;
using System.Net;
using System.Threading.Tasks;
#if (shared)
using CleanArchitectureTemplate.Shared.Kernel.Exceptions;
#else
using CleanArchitectureTemplate.Core.Exceptions;
using CleanArchitectureTemplate.Infrastructure.Exceptions;
#endif
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
#if (shared)
using ApplicationException = CleanArchitectureTemplate.Shared.Kernel.Exceptions.ApplicationException;
#else
using ApplicationException = CleanArchitectureTemplate.Application.Exceptions.ApplicationException;
#endif

namespace CleanArchitectureTemplate.Api.Middlewares
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
