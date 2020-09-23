#if (postgres)
using System;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Queries;
using CleanArchitectureTemplate.Infrastructure.Mappings;
using CleanArchitectureTemplate.Infrastructure.Persistence.EF;
using CleanArchitectureTemplate.Infrastructure.Persistence.EF.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models;
using Convey.CQRS.Queries;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Queries.Handlers
{
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
    {
        private readonly IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> _repository;

        public GetOrderHandler(IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> repository) 
            => _repository = repository;

        public async Task<OrderDto> HandleAsync(GetOrder query)
            => (await _repository.GetAsync(x => x.Id == query.Id))
                ?.AsDto();
    }
}
#endif