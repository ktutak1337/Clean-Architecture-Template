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
                throw new Exception($"Order with Id: {command.Id} already exists.");
            }
            
            var address = new Address("city", "street", "province", "country", "00-000");

            var items = new List<OrderItem>() 
            {
                new OrderItem("item", 10, 3.14m),
                new OrderItem("item2", 2, 2.99m)
            };

            order = new Order(command.Id, command.BuyerId, address, items, OrderStatus.Pending);
            
            await _ordersRepository.AddAsync(order);
        }
    }
}
