using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetOrdersHandler : IRequestHandler<GetOrders, IEnumerable<OrderDto>>
    {
        private readonly IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> _repository;

        public GetOrdersHandler(IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> repository)
            => _repository = repository;

        public async Task<IEnumerable<OrderDto>> Handle(GetOrders query, CancellationToken cancellationToken)
            => (await _repository.FindAsync(_ => true, x => x.Items))
                .Select(order => order.AsDto());
    }
    #else
    public class GetOrdersHandler : IQueryHandler<GetOrders, IEnumerable<OrderDto>>
    {
        private readonly IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> _repository;

        public GetOrdersHandler(IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> repository)
            => _repository = repository;

        public async Task<IEnumerable<OrderDto>> HandleAsync(GetOrders query)
            => (await _repository.FindAsync(_ => true, x => x.Items))
                .Select(order => order.AsDto());
    }
    #endif
}
#endif
