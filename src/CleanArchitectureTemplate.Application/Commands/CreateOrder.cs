using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CleanArchitectureTemplate.Application.Commands.WriteModels;
using CleanArchitectureTemplate.Core;
using Convey.CQRS.Commands;

namespace CleanArchitectureTemplate.Application.Commands
{
    public record CreateOrder([Required] Guid BuyerId, [Required] AddressWriteModel ShippingAddress, [Required] IEnumerable<OrderItemWriteModel> Items) : ICommand
    {
        public Guid Id { get; init; } = new OrderId(Guid.NewGuid());
    }
}
