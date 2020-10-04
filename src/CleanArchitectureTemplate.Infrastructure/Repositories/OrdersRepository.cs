using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Core.Aggregates;
using CleanArchitectureTemplate.Core.Repositories;
using CleanArchitectureTemplate.Core.ValueObjects;
using CleanArchitectureTemplate.Core.Entities;
using CleanArchitectureTemplate.Core.Types;
using CleanArchitectureTemplate.Infrastructure.Exceptions;

namespace CleanArchitectureTemplate.Infrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private static readonly ISet<Order> _orders = new HashSet<Order>
        {
            new Order(Guid.NewGuid(),
                Guid.NewGuid(),
                new Address("Warsaw", "Złota 44", "Masovia", "Poland", "00-120"), 
                new List<OrderItem>() 
                {
                    new OrderItem("Milk", 10, 1.99m),
                    new OrderItem("Cheese", 2, 3.49m)
                }, 
                OrderStatus.Paid),

            new Order(Guid.NewGuid(),
                Guid.NewGuid(),
                new Address("Los Angeles", "111 N Hill St", "California", "United States", "CA 90012"), 
                new List<OrderItem>() 
                {
                    new OrderItem("Donut", 4, 0.99m),
                    new OrderItem("Coffee", 2, 2.99m)
                }, 
                OrderStatus.Paid)    
        };

        public async Task<Order> GetAsync(Guid id) 
            => await Task.FromResult(_orders.SingleOrDefault(order => order.Id == id));

        public async Task<IEnumerable<Order>> BrowseAsync()
            => await Task.FromResult(_orders);

        public async Task AddAsync(Order order)
        {
            if (order is null)
            {
                throw new EmptyOrderException();
            }

            _orders.Add(order);

            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Order order)
        {
            if (order is null)
            {
                throw new EmptyOrderException();
            }

            var existingOrder = _orders.SingleOrDefault(existingOrder => existingOrder.Id == order.Id);

            if(existingOrder is null)
            {
                throw new OrderNotFoundException(order.Id);
            }

            existingOrder.UpdateOrder(order);
            
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = _orders.SingleOrDefault(order => order.Id == id);
            
            if(order is null)
            {
                throw new OrderNotFoundException(order.Id);
            }

            _orders.Remove(order);

            await Task.CompletedTask;
        }
    }
}
