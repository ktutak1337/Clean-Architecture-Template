using System;
using System.Collections.Generic;
using CleanArchitectureTemplate.Application.Commands.WriteModels;
using Convey.CQRS.Commands;

namespace CleanArchitectureTemplate.Application.Commands
{
    public record UpdateOrder(Guid Id, Guid BuyerId, AddressWriteModel ShippingAddress, IEnumerable<OrderItemWriteModel> Items, string Status) : ICommand;
}
