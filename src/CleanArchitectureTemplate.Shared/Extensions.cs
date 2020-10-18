using CleanArchitectureTemplate.Shared.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            services.AddTransient<IDispatcher, Dispatcher>();
            
            return services;
        }
    }
}
