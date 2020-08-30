using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Queries;
using CleanArchitectureTemplate.Core.Repositories;
using CleanArchitectureTemplate.Infrastructure.Mappings;
using Convey.CQRS.Queries;

namespace CleanArchitectureTemplate.Infrastructure.Queries.Handlers
{
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
    {
        private readonly IOrdersRepository _repository;

        public GetOrderHandler(IOrdersRepository repository)
            => _repository = repository;

        public async Task<OrderDto> HandleAsync(GetOrder query) 
            => (await _repository.GetAsync(query.Id))
                ?.AsDto();
    }
}
