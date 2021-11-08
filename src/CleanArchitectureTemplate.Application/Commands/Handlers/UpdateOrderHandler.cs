using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Core.Exceptions;
using CleanArchitectureTemplate.Core.Repositories;
using CleanArchitectureTemplate.Core.Types;
#if(mediatr)
using MediatR;
#else
using Convey.CQRS.Commands;
#endif
using Microsoft.Extensions.Logging;

namespace CleanArchitectureTemplate.Application.Commands.Handlers
{
    #if(mediatr)
    public class UpdateOrderHandler : IRequestHandler<UpdateOrder>
    {
        private readonly IOrdersRepository _repository;
        private readonly ILogger<UpdateOrderHandler> _logger;

        public UpdateOrderHandler(IOrdersRepository repository, ILogger<UpdateOrderHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateOrder command, CancellationToken cancellationToken)
        {
            if(!Enum.TryParse<OrderStatus>(command.Status, true, out var status))
            {
                throw new InvalidOrderStatusException(command.Status);
            }

            var order = await _repository.GetAsync(command.Id);

            if(order is null)
            {
                throw new OrderNotFoundException(command.Id);
            }

            order.Update(command.BuyerId, command.ShippingAddress.AsValueObject(), command.Items.AsEntities(), status);

            await _repository.UpdateAsync(order);

            _logger.LogInformation($"Order with ID: '{command.Id}' has been updated.");

            return Unit.Value;
        }
    }
    #else
    public class UpdateOrderHandler : ICommandHandler<UpdateOrder>
    {
        private readonly IOrdersRepository _repository;
        private readonly ILogger<UpdateOrderHandler> _logger;

        public UpdateOrderHandler(IOrdersRepository repository, ILogger<UpdateOrderHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task HandleAsync(UpdateOrder command)
        {
            if(!Enum.TryParse<OrderStatus>(command.Status, true, out var status))
            {
                throw new InvalidOrderStatusException(command.Status);
            }

            var order = await _repository.GetAsync(command.Id);

            if(order is null)
            {
                throw new OrderNotFoundException(command.Id);
            }

            order.Update(command.BuyerId, command.ShippingAddress.AsValueObject(), command.Items.AsEntities(), status);

            await _repository.UpdateAsync(order);
            _logger.LogInformation($"Order with ID: '{command.Id}' has been updated.");
        }
    }
    #endif
}
