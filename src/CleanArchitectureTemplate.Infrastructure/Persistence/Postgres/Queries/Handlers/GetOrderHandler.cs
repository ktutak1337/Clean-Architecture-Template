using System;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Queries;
using CleanArchitectureTemplate.Infrastructure.Mappings;
#if (shared && postgres)
using CleanArchitectureTemplate.Shared.Persistence.EF.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models;
#else
using CleanArchitectureTemplate.Infrastructure.Persistence.EF.Repositories;
#endif
#if (postgres && !shared)
using CleanArchitectureTemplate.Infrastructure.Persistence.EF;
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models;
#endif
#if(mediatr)
using MediatR;
using System.Threading;
#else
using Convey.CQRS.Queries;
#endif
#if (postgres)

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Queries.Handlers
{
    #if(mediatr)
    public class GetOrderHandler : IRequestHandler<GetOrder, OrderDto>
    {
        private readonly IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> _repository;

        public GetOrderHandler(IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> repository)
            => _repository = repository;

        public async Task<OrderDto> Handle(GetOrder query, CancellationToken cancellationToken)
            => (await _repository.GetAsync(x => x.Id == query.Id))
                ?.AsDto();
    }
    #else
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
    {
        private readonly IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> _repository;

        public GetOrderHandler(IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> repository)
            => _repository = repository;

        public async Task<OrderDto> HandleAsync(GetOrder query)
            => (await _repository.GetAsync(x => x.Id == query.Id))
                ?.AsDto();
    }
    #endif
}
#endif
