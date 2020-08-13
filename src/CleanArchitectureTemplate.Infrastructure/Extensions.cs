using System;
using AutoMapper;
using CleanArchitectureTemplate.Core.Repositories;
using CleanArchitectureTemplate.Infrastructure.Mappings;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Repositories;
using Convey;
using Convey.Persistence.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();
            builder.Services.AddAutoMapper(typeof(DomainToDocumentProfile));

            return builder
                .AddMongo()
                .AddMongoRepository<OrderDocument, Guid>("orders");
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app) 
            => app;
    }
}
