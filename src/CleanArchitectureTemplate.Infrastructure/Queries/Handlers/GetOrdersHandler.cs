using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Queries;
using CleanArchitectureTemplate.Core.Repositories;
using Convey.CQRS.Queries;
using CleanArchitectureTemplate.Infrastructure.Mappings;

namespace CleanArchitectureTemplate.Infrastructure.Queries.Handlers
{
    public class GetOrdersHandler : IQueryHandler<GetOrders, IEnumerable<OrderDto>>
    {
        private readonly IOrdersRepository _repository;

        public GetOrdersHandler(IOrdersRepository repository) 
            => _repository = repository;

        public async Task<IEnumerable<OrderDto>> HandleAsync(GetOrders query) 
            => (await _repository.BrowseAsync())
                ?.Select(order => order.AsDto());
    }
}
