using CleanArchitectureTemplate.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace CleanArchitectureTemplate.Api
{
    public static class Extensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ErrorHandlerMiddleware));
    }
}
