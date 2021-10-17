using Microsoft.AspNetCore.Builder;

namespace CleanArchitectureTemplate.Shared.Kernel.Exceptions
{
    public static class Extensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ErrorHandlerMiddleware));
    }
}
