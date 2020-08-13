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
        public Guid Id { get; }
        public Guid BuyerId { get; }
        public Address Address { get; }
        public IEnumerable<OrderItem> Items { get; }

        public CreateOrder(Guid buyerId, Address address, IEnumerable<OrderItem> items)
        {
            Id = new OrderId(Guid.NewGuid());
            BuyerId = buyerId;
            Address = address;
            Items = items;
        }
    }
}
