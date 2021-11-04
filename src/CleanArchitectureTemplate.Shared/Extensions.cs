using System;
using System.Linq;
using System.Text.RegularExpressions;
#if(!mediatr)
using CleanArchitectureTemplate.Shared.Dispatchers;
#endif
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Shared
{
    public static class Extensions
    {
        private const string CorrelationIdKey = "correlation-id";

        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            #if(!mediatr)
            services.AddTransient<IDispatcher, Dispatcher>();
            #endif

            return services;
        }

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
            where TModel : new()
        {
            if (!string.IsNullOrWhiteSpace(sectionName))
            {
                var model = new TModel();
                configuration.GetSection(sectionName).Bind(model);

                return model;
            }

            return default(TModel);
        }

        public static TModel GetOptions<TModel>(this IServiceCollection services, string sectionName)
            where TModel : new()
        {
            if (!string.IsNullOrWhiteSpace(sectionName))
            {
                using var serviceProvider = services.BuildServiceProvider();
                var configuration = serviceProvider.GetService<IConfiguration>();
                return configuration.GetOptions<TModel>(sectionName);
            }

            return default(TModel);
        }

        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
            => app.Use((ctx, next) =>
            {
                ctx.Items.Add(CorrelationIdKey, Guid.NewGuid());
                return next();
            });

        public static Guid? TryGetCorrelationId(this HttpContext context)
            => context.Items.TryGetValue(CorrelationIdKey, out var id) ? (Guid) id : null;

        public static string GetUserIpAddress(this HttpContext context)
        {
            if (context is null)
            {
                return string.Empty;
            }

            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            if (context.Request.Headers.TryGetValue("x-forwarded-for", out var forwardedFor))
            {
                var ipAddresses = forwardedFor.ToString().Split(",", StringSplitOptions.RemoveEmptyEntries);

                if (ipAddresses.Any())
                {
                    ipAddress = ipAddresses[0];
                }
            }

            return ipAddress ?? string.Empty;
        }

        public static string ToSnakeCase(this string input)
            => Regex.Replace(
                Regex.Replace(
                    Regex.Replace(input, @"([\p{Lu}]+)([\p{Lu}][\p{Ll}])", "$1_$2"), @"([\p{Ll}\d])([\p{Lu}])", "$1_$2"), @"[-\s]", "_").ToLower();
    }
}
