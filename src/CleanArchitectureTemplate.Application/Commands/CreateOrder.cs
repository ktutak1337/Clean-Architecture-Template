using System;
using System.Collections.Generic;
using CleanArchitectureTemplate.Application.Commands.WriteModels;
using CleanArchitectureTemplate.Core;
using Convey.CQRS.Commands;

namespace CleanArchitectureTemplate.Application.Commands
{
    public class CreateOrder : ICommand
    {
        public Guid Id { get; set; } = new OrderId(Guid.NewGuid());
        public Guid BuyerId { get; set; }
        public AddressWriteModel Address { get; set; }
        public IEnumerable<OrderItemWriteModel> Items { get; set; }
    }
}
