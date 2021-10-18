using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CleanArchitectureTemplate.Infrastructure.Swagger
{
    public static class Extensions
    {
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

                if(settings.CommentsEnabled)
                {
                    var filePath = Path.Combine(System.AppContext.BaseDirectory, "CleanArchitectureTemplate.Api.xml");
                    setup.IncludeXmlComments(filePath);
                }

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
    }
}
