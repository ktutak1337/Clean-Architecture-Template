using System;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Queries;
using CleanArchitectureTemplate.Infrastructure.Mappings;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Queries.Handlers
{
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
    {
        private readonly IMongoRepository<OrderDocument, Guid> _repository;

        public GetOrderHandler(IMongoRepository<OrderDocument, Guid> repository)
            => _repository = repository;

        public async Task<OrderDto> HandleAsync(GetOrder query)
        {
            var document = await _repository.GetAsync(x => x.Id == query.Id);

            return document?.AsDto();
        }
    }
}
