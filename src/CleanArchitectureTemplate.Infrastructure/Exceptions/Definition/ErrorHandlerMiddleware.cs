using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CleanArchitectureTemplate.Infrastructure.Exceptions.Definition
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly IExceptionCompositionRoot _exceptionCompositionRoot;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(IExceptionCompositionRoot exceptionCompositionRoot, ILoggerFactory loggerFactory)
        {
            _exceptionCompositionRoot = exceptionCompositionRoot;
            _logger = loggerFactory?.CreateLogger<ErrorHandlerMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await HandleErrorAsync(context, exception);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var errorResponse = _exceptionCompositionRoot.Map(exception);
            context.Response.StatusCode = (int) (errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);
            var response = errorResponse?.Response;

            if (response is null)
            {
                return;
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
