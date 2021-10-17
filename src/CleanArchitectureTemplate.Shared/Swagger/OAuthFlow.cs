using System;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace CleanArchitectureTemplate.Shared.Swagger
{
    public sealed class OAuthFlow
    {
        public static OpenApiOAuthFlow Setup(SwaggerSettings settings)
            => string.IsNullOrWhiteSpace(settings.OAuth2.RefreshTokenUrl)
                ? new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(settings.OAuth2.AuthorizationUrl),
                    TokenUrl = new Uri(settings.OAuth2.TokenUrl),
                    Scopes = settings.OAuth2.Scopes
                        .ToDictionary(scope => scope.Name, scope => scope.Description)
                }
                : new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(settings.OAuth2.AuthorizationUrl),
                    TokenUrl = new Uri(settings.OAuth2.TokenUrl),
                    RefreshUrl = new Uri(settings.OAuth2.RefreshTokenUrl),
                    Scopes = settings.OAuth2.Scopes
                        .ToDictionary(scope => scope.Name, scope => scope.Description)
                };
    }
}
