using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Core.Aggregates;

namespace CleanArchitectureTemplate.Core.Repositories
{
    public interface IOrdersRepository
    {
        Task<Order> GetAsync(Guid id);
        Task<IEnumerable<Order>> BrowseAsync();
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
    }
}
