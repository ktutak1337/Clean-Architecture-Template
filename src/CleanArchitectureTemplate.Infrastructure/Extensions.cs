using System;
using CleanArchitectureTemplate.Core.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Repositories;
using Convey;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();

            return builder
                .AddMongo()
                .AddMongoRepository<OrderDocument, Guid>("orders");
        }
    }
}
