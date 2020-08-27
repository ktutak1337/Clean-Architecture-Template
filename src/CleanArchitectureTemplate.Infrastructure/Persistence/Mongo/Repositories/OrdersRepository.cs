using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Core.Aggregates;
using CleanArchitectureTemplate.Core.Repositories;
using CleanArchitectureTemplate.Infrastructure.Mappings;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;
using Convey.Persistence.MongoDB;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Repositories
{
    public sealed class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoRepository<OrderDocument, Guid> _repository;

        public OrdersRepository(IMongoRepository<OrderDocument, Guid> repository) 
            => _repository = repository;

        public async Task<Order> GetAsync(Guid id)
        {
           var document = await _repository.GetAsync(id);

           return document?.AsEntity();
        }
        public Task<IEnumerable<Order>> BrowseAsync()
            => throw new NotImplementedException();

        public async Task AddAsync(Order order) 
            => await _repository.AddAsync(order.AsDocument());

        public async Task UpdateAsync(Order order)
            => await _repository.UpdateAsync(order.AsDocument());

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}