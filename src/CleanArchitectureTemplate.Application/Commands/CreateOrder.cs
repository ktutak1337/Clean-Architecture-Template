using System;
using System.Collections.Generic;
using CleanArchitectureTemplate.Core;
using CleanArchitectureTemplate.Core.Entities;
using CleanArchitectureTemplate.Core.ValueObjects;
using Convey.CQRS.Commands;

namespace CleanArchitectureTemplate.Application.Commands
{
    public class CreateOrder : ICommand
    {
        public Guid Id { get; set; } = new OrderId(Guid.NewGuid());
        public Guid BuyerId { get; set; }
        public Address Address { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}
