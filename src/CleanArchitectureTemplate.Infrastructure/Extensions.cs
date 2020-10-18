#if (swagger || mongo || postgres)
using System;
#endif
#if (!shared)
using CleanArchitectureTemplate.Application.Services;
#endif
using CleanArchitectureTemplate.Core.Repositories;
#if (mongo)
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Repositories;
#endif
#if (!shared)
using CleanArchitectureTemplate.Infrastructure.Services;
#endif
#if (!mongo && !postgres)
using CleanArchitectureTemplate.Infrastructure.Repositories;
#endif
#if (swagger)
using CleanArchitectureTemplate.Infrastructure.Swagger;
#endif
using Convey;
#if (mongo)
using Convey.Persistence.MongoDB;
#endif
#if (swagger)
using Microsoft.AspNetCore.Builder;
#endif
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#if (shared && postgres)
using Microsoft.EntityFrameworkCore;
using CleanArchitectureTemplate.Shared.Infrastructure.Persistence.EF.Repositories;
using CleanArchitectureTemplate.Shared.Infrastructure.Persistence.Types;
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres;
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models;
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Repositories;
#endif
#if (postgres && !shared)
using Microsoft.EntityFrameworkCore;
using CleanArchitectureTemplate.Infrastructure.Persistence.EF;
using CleanArchitectureTemplate.Infrastructure.Persistence.EF.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres;
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models;
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence.Types;
#endif
#if (swagger)
using Microsoft.OpenApi.Models;
#endif

namespace CleanArchitectureTemplate.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            #if (mongo && postgres)
            builder.Services.AddTransient<IOrdersRepository, Persistence.Mongo.Repositories.OrdersRepository>();
            #else
            builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();
            #endif
            #if (!shared)
            builder.Services.AddTransient<IDispatcher, Dispatcher>();
            #endif
            #if (mongo)
            return builder
                .AddMongo()
                .AddMongoRepository<OrderDocument, Guid>("orders");
            #else
            return builder;
            #endif 
        }
        
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            #if (swagger)
            services.AddSwaggerDocs();
            #endif
            #if (postgres)
            services.AddSingleton(services.GetOptions<PostgresSettings>("postgres"));
            services.AddEntityFrameworkNpgsql()
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddDatabaseContext<CleanArchitectureTemplateDbContext>();
            services.AddEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext>();
            #endif
            #if (mongo && postgres)
            services.AddTransient<IOrdersRepository, Persistence.Postgres.Repositories.OrdersRepository>();
            #endif
            return services;
        }
        
        #if (postgres)
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
        #endif

        #if (postgres)
        public static IServiceCollection AddEntityFrameworkRepository<TEntity, TIdentifiable, TDatabseContext>(this IServiceCollection services)
            where TEntity : class, IIdentifiable<TIdentifiable>
            where TDatabseContext : DbContext
                => services.AddTransient<IEntityFrameworkRepository<TEntity, TIdentifiable, TDatabseContext>, EntityFrameworkRepository<TEntity, TIdentifiable, TDatabseContext>>();
        #endif
        #if (swagger)

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder)
            => builder.UseSwaggerDocs();

        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
        {
            var settings = services.GetOptions<SwaggerSettings>("swagger");

            if(!settings.Enabled)
            {
                return services;
            }

            services.AddSingleton(new SwaggerSettings());
            return services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc(settings.Name, new OpenApiInfo { Title = settings.Title, Version = settings.Version });
                
                if (settings.Authorization)
                {
                    setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        Description = "JWT Authorization header using the Bearer scheme (Example: Bearer {token}).",
                    });

                    if(!(settings.OAuth2 is null))
                    {
                        setup.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                        {
                            Flows = new OpenApiOAuthFlows
                            {
                                Implicit = OAuthFlow.Setup(settings),
                                Password = OAuthFlow.Setup(settings),
                                ClientCredentials = OAuthFlow.Setup(settings),
                                AuthorizationCode = OAuthFlow.Setup(settings)
                            },
                            In = ParameterLocation.Header,
                            Name = "Authorization",
                            Type = SecuritySchemeType.OAuth2
                        });
                    }

                    setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                }
            });
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder)
        {
            var settings = builder.ApplicationServices.GetService<IConfiguration>()
                .GetOptions<SwaggerSettings>("swagger");
            
            if (!settings.Enabled)
            {
                return builder;
            }

            var routePrefix = string.IsNullOrWhiteSpace(settings.RoutePrefix) ? "swagger ": settings.RoutePrefix;

            builder.UseStaticFiles()
                .UseSwagger(setup => setup.RouteTemplate = routePrefix + "/{documentName}/swagger.json");

            return builder.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint($"/{routePrefix}/{settings.Name}/swagger.json", settings.Title);
                setup.RoutePrefix = routePrefix;
            });
        }
        #endif
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
    }
}
