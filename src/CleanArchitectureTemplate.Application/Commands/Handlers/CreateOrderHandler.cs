using Convey.CQRS.Commands;
using CleanArchitectureTemplate.Core.Repositories;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Core.Aggregates;
using CleanArchitectureTemplate.Core.ValueObjects;
using System.Collections.Generic;
using CleanArchitectureTemplate.Core.Entities;
using CleanArchitectureTemplate.Core.Types;
using CleanArchitectureTemplate.Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureTemplate.Application.Commands.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ILogger<CreateOrderHandler> _logger;

        public CreateOrderHandler(IOrdersRepository ordersRepository, ILogger<CreateOrderHandler> logger)
        {
            _ordersRepository = ordersRepository;
            _logger = logger;
        }

        public async Task HandleAsync(CreateOrder command)
        {
            var order = await _ordersRepository.GetAsync(command.Id);

            if(order is not null)
            {
                throw new OrderAlreadyExistsException(command.Id);
            }

            order = new Order(command.Id, command.BuyerId, command.ShippingAddress.AsValueObject(), command.Items.AsEntities(), OrderStatus.Pending);

            await _ordersRepository.AddAsync(order);

            _logger.LogInformation($"Order with ID: {order.Id} has been created.");
        }
    }
}
