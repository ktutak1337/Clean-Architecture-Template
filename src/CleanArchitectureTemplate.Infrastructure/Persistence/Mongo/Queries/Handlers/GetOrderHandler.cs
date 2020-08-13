using System.Threading.Tasks;
using AutoMapper;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Queries;
using CleanArchitectureTemplate.Core.Aggregates;
using CleanArchitectureTemplate.Core.Repositories;
using CleanArchitectureTemplate.Infrastructure.Mappings;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;
using Convey.CQRS.Queries;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Queries.Handlers
{
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
    {
        private readonly IOrdersRepository _repository;
        private readonly IMapper _mapper;

        public GetOrderHandler(IOrdersRepository repository, IMapper mapper)
            => (_repository, _mapper) = (repository, mapper);

        public async Task<OrderDto> HandleAsync(GetOrder query) 
            => (await _repository.GetAsync(query.Id))
                .MapSingle<Order, OrderDto>(_mapper);
    }
}
