using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Queries;
using CleanArchitectureTemplate.Core.Repositories;
using CleanArchitectureTemplate.Infrastructure.Mappings;
#if(mediatr)
using MediatR;
#else
using Convey.CQRS.Queries;
#endif

namespace CleanArchitectureTemplate.Infrastructure.Queries.Handlers
{
    #if(mediatr)
    public class GetOrderHandler : IRequestHandler<GetOrder, OrderDto>
    {
        private readonly IOrdersRepository _repository;

        public GetOrderHandler(IOrdersRepository repository)
            => _repository = repository;

        public async Task<OrderDto> Handle(GetOrder query, CancellationToken cancellationToken)
            => (await _repository.GetAsync(query.Id))
                ?.AsDto();
    }
    #else
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
    {
        private readonly IOrdersRepository _repository;

        public GetOrderHandler(IOrdersRepository repository)
            => _repository = repository;

        public async Task<OrderDto> HandleAsync(GetOrder query)
            => (await _repository.GetAsync(query.Id))
                ?.AsDto();
    }
    #endif
}
