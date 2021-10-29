using System;
#if (!shared)
using CleanArchitectureTemplate.Infrastructure.Persistence.EF;
#else
using CleanArchitectureTemplate.Shared;
using CleanArchitectureTemplate.Shared.Persistence.EF;
#endif
#if (!noSampleCode)
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models;
#endif
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Postgres
{
    public static class Extensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services)
        {
            services.AddSingleton(services.GetOptions<PostgresSettings>("postgres"));
            services.AddEntityFrameworkNpgsql()
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddDatabaseContext<CleanArchitectureTemplateDbContext>();

            services.AddPostgresRepositories();

            return services;
        }

        public static IServiceCollection AddPostgresRepositories(this IServiceCollection services)
        {
            #if (!noSampleCode)
            services.AddEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext>();
            #endif

            return services;
        }

        public static IServiceCollection AddDatabaseContext<TDatabseContext>(this IServiceCollection services)
            where TDatabseContext : DbContext
        {
            var settings = services.GetOptions<PostgresSettings>("postgres");

            services.AddDbContext<TDatabseContext>(options =>
            {
                if(settings.InMemory)
                {
                    options.UseInMemoryDatabase(databaseName: settings.InMemoryDatabaseName);

                    return;
                }

                options.UseNpgsql(settings.ConnectionString);
                options.EnableSensitiveDataLogging();
            });

            return services;
        }
    }
}
