using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Queries;
using CleanArchitectureTemplate.Infrastructure.Mappings;
#if (shared && postgres)
using CleanArchitectureTemplate.Shared.Infrastructure.Persistence.EF.Repositories;
#else
using CleanArchitectureTemplate.Infrastructure.Persistence.EF.Repositories;
#endif
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models;
using Convey.CQRS.Queries;
#if (postgres)

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Queries.Handlers
{
    public class GetOrdersHandler : IQueryHandler<GetOrders, IEnumerable<OrderDto>>
    {
        private readonly IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> _repository;

        public GetOrdersHandler(IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> repository) 
            => _repository = repository;

        public async Task<IEnumerable<OrderDto>> HandleAsync(GetOrders query)
            => (await _repository.FindAsync(_ => true, x => x.Items))
                .Select(order => order.AsDto());
    }
}
#endif