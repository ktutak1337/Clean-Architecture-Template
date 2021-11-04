using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Queries;
using CleanArchitectureTemplate.Core.Repositories;
using System.Threading;
#if (mediatr)
using MediatR;
#else
using Convey.CQRS.Queries;
#endif
using CleanArchitectureTemplate.Infrastructure.Mappings;

namespace CleanArchitectureTemplate.Infrastructure.Queries.Handlers
{
    #if (mediatr)
    public class GetOrdersHandler : IRequestHandler<GetOrders, IEnumerable<OrderDto>>
    {
        private readonly IOrdersRepository _repository;

        public GetOrdersHandler(IOrdersRepository repository)
            => _repository = repository;

        public async Task<IEnumerable<OrderDto>> Handle(GetOrders query, CancellationToken cancellationToken)
            => (await _repository.BrowseAsync())
                ?.Select(order => order.AsDto());
    }
    #else
    public class GetOrdersHandler : IQueryHandler<GetOrders, IEnumerable<OrderDto>>
    {
        private readonly IOrdersRepository _repository;

        public GetOrdersHandler(IOrdersRepository repository)
            => _repository = repository;

        public async Task<IEnumerable<OrderDto>> HandleAsync(GetOrders query)
            => (await _repository.BrowseAsync())
                ?.Select(order => order.AsDto());
    }
    #endif
}
