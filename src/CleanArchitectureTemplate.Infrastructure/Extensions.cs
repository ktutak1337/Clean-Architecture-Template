using System;
#if (!shared && !mediatr)
using CleanArchitectureTemplate.Application.Dispatchers;
using CleanArchitectureTemplate.Infrastructure.Dispatchers;
#endif
#if (!noSampleCode)
using CleanArchitectureTemplate.Core.Repositories;
#endif
#if (mongo && !noSampleCode)
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Repositories;
#endif
#if (!shared)
using CleanArchitectureTemplate.Infrastructure.Contexts;
using CleanArchitectureTemplate.Infrastructure.Exceptions.Definition;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
#endif
#if (shared)
using CleanArchitectureTemplate.Shared.Exceptions;
using CleanArchitectureTemplate.Shared;
using CleanArchitectureTemplate.Shared.Contexts;
using CleanArchitectureTemplate.Shared.Swagger;
#endif
#if (!shared && serilog)
using CleanArchitectureTemplate.Infrastructure.Logging;
#endif
#if (shared && serilog)
using CleanArchitectureTemplate.Shared.Logging;
#endif
#if (!mongo && !postgres && !noSampleCode)
using CleanArchitectureTemplate.Infrastructure.Repositories;
#endif
#if (swagger && !shared)
using CleanArchitectureTemplate.Infrastructure.Swagger;
#endif
#if (mongo)
using Convey;
using Convey.Persistence.MongoDB;
#endif
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Application;
#if (shared && postgres)
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres;
#endif
#if (postgres && !shared)
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres;
#endif
#if (postgres && !noSampleCode)
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Repositories;
#endif
#if(mediatr)
using MediatR;
#endif

namespace CleanArchitectureTemplate.Infrastructure
{
    public static class Extensions
    {
        private const string CorrelationIdKey = "correlation-id";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddErrorHandling();
        #if (shared)
            services.AddShared();
        #endif
            services.AddContext();
        #if (swagger)
            services.AddSwaggerDocs();
        #endif
        #if (postgres)
            services.AddPostgres();
        #endif
        #if (mediatr)
            services.AddMediatR(typeof(Extensions).Assembly, typeof(Application.Extensions).Assembly);
        #endif
        #if (mongo && postgres && !noSampleCode)
            services.AddTransient<IOrdersRepository, Persistence.Postgres.Repositories.OrdersRepository>();
        #endif
            #if (mongo && !mediatr)
            services
                .AddConvey()
                .AddApplication()
                .AddInfrastructure();
            #endif
            #if (mongo && mediatr)
            services
                .AddConvey()
                .AddInfrastructure();
            #endif

            services.AddSingleton(services.GetOptions<AppSettings>("app"));
        #if (!shared && !mediatr)
            services.AddSingleton<IDispatcher, Dispatcher>();
        #endif

        #if (!noSampleCode)
            #if (mongo && postgres)
            services.AddTransient<IOrdersRepository, Persistence.Mongo.Repositories.OrdersRepository>();
            #else
            services.AddTransient<IOrdersRepository, OrdersRepository>();
            #endif
        #endif

            return services;
        }

        #if (mongo)
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            #if (!noSampleCode)
            builder
                .AddMongo()
                .AddMongoRepository<OrderDocument, Guid>("orders");
            #else
            builder
                .AddMongo();
            #endif

            return builder;
        }
        #endif

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseCorrelationId();

            app.UseErrorHandling();

        #if (swagger)
            app.UseSwaggerDocs();

        #endif
            app.UseHttpsRedirection();

            app.UseContext();

        #if (serilog)
            app.UseLogging();

        #endif
            app.UseRouting();

            app.UseAuthorization();

            return app;
        }

        #if (!shared)
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
        #endif
    }
}
