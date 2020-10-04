using Convey.CQRS.Commands;
using CleanArchitectureTemplate.Application.Commands;
using CleanArchitectureTemplate.Core.Repositories;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Core.Aggregates;
using System;
using CleanArchitectureTemplate.Core.ValueObjects;
using System.Collections.Generic;
using CleanArchitectureTemplate.Core.Entities;
using CleanArchitectureTemplate.Core.Types;
using CleanArchitectureTemplate.Application.Exceptions;

namespace CleanArchitectureTemplate.Application.Commands.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IOrdersRepository _ordersRepository;

        public CreateOrderHandler(IOrdersRepository ordersRepository) 
            => _ordersRepository = ordersRepository;

        public async Task HandleAsync(CreateOrder command)
        {
            var order = await _ordersRepository.GetAsync(command.Id);
            
            if(!(order is null))
            {
                throw new OrderAlreadyExistsException(command.Id);
            }
           
            var address = new Address("New York", "20 W 34th St", "New York", "United States", "NY 10001");

            var items = new List<OrderItem>() 
            {
                new OrderItem("ice creams", 2, 4.00m),
                new OrderItem("Coffee", 2, 2.99m),
                new OrderItem("Apple pie", 2, 5.49m),
            };

            order = new Order(command.Id, command.BuyerId, address, items, OrderStatus.Pending);
            
            await _ordersRepository.AddAsync(order);
        }
    }
}
