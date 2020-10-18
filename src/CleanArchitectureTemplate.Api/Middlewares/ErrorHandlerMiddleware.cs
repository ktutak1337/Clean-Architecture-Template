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
using Newtonsoft.Json;
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

        private static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var errorCode = "Error";
            var message = exception.Message;
            var statusCode = HttpStatusCode.BadRequest;
            
            switch(exception)
            {
                case InfrastructureException ex:
                    errorCode = ex.Code;
                    message = ex.Message;
                    break;

                case ApplicationException ex:
                    errorCode = ex.Code;
                    message = ex.Message;
                    break;

                case DomainException ex:
                    errorCode = ex.Code;
                    message = ex.Message;
                    break;
            }

            var response = new { code = errorCode, message = message };
            var json = JsonConvert.SerializeObject(response);
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(json);
        }
    }
}
