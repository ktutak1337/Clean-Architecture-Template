#if (mongo)
using System;
using System.Linq;
using System.Collections.Generic;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Queries;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Infrastructure.Mappings;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Queries.Handlers
{
    public class GetOrdersHandler : IQueryHandler<GetOrders, IEnumerable<OrderDto>>
    {
        private readonly IMongoRepository<OrderDocument, Guid> _repository;

        public GetOrdersHandler(IMongoRepository<OrderDocument, Guid> repository)
            => _repository = repository;

        public async Task<IEnumerable<OrderDto>> HandleAsync(GetOrders query)
            => (await _repository.FindAsync(_ => true))
                .Select(order => order.AsDto());
    }
}
#endif