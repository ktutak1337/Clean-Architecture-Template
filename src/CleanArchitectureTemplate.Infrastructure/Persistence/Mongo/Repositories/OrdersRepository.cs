using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using CleanArchitectureTemplate.Core.Aggregates;
using CleanArchitectureTemplate.Core.Repositories;
using CleanArchitectureTemplate.Infrastructure.Mappings;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;
using Convey.Persistence.MongoDB;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoRepository<OrderDocument, Guid> _repository;
        private readonly IMapper _mapper;

        public OrdersRepository(IMongoRepository<OrderDocument, Guid> repository, IMapper mapper) 
            => (_repository, _mapper) = (repository, mapper);

        public async Task<Order> GetAsync(Guid id)
            => (await _repository.GetAsync(id))
                .MapSingle<OrderDocument, Order>(_mapper);

        public Task<IEnumerable<Order>> BrowseAsync()
            => throw new NotImplementedException();

        public async Task AddAsync(Order order) 
            => await _repository.AddAsync(order
                .MapSingle<Order, OrderDocument>(_mapper));

        public async Task UpdateAsync(Order order)
            => await _repository.UpdateAsync(order
                .MapSingle<Order, OrderDocument>(_mapper));

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}
