using Microsoft.AspNetCore.Builder;

namespace CleanArchitectureTemplate.Infrastructure.Exceptions.Definition
{
    public static class Extensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ErrorHandlerMiddleware));
    }
}
