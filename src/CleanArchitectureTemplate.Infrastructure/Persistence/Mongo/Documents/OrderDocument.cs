using System;
using System.Collections.Generic;
using CleanArchitectureTemplate.Core.Types;
using Convey.Types;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents
{
    public class OrderDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public AddressDocument Address { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<OrderItemDocument> Items { get; set; }
        public int Version { get; set; }
    }
}
