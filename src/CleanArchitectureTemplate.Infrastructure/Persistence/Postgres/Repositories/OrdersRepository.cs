using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Core.Aggregates;
using CleanArchitectureTemplate.Core.Repositories;
using CleanArchitectureTemplate.Infrastructure.Mappings;
#if (shared && postgres)
using CleanArchitectureTemplate.Shared.Infrastructure.Persistence.EF.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models;
#else
using CleanArchitectureTemplate.Infrastructure.Persistence.EF.Repositories;
#endif
#if (postgres && !shared)
using CleanArchitectureTemplate.Infrastructure.Persistence.EF;
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models;
#endif
#if (postgres)

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Repositories
{
    public sealed class OrdersRepository : IOrdersRepository
    {
        private readonly IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> _repository;

        public OrdersRepository(IEntityFrameworkRepository<OrderModel, Guid, CleanArchitectureTemplateDbContext> repository)
            => _repository = repository;
        
        public async Task<Order> GetAsync(Guid id)
            => (await _repository.GetAsync(id, x => x.Items))
                ?.AsEntity();

        public async Task<IEnumerable<Order>> BrowseAsync()
            => (await _repository.FindAsync(_ => true, x => x.Items))
                ?.Select(order => order.AsEntity());

        public async Task AddAsync(Order order)
            => await _repository.AddAsync(order.AsDatabaseModel());

        public async Task UpdateAsync(Order order)
            => await _repository.UpdateAsync(order.AsDatabaseModel());

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}
#endif