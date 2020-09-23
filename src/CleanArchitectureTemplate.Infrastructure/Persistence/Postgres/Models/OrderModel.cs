using System;
using System.Collections.Generic;
using CleanArchitectureTemplate.Core.Types;
using CleanArchitectureTemplate.Infrastructure.Persistence.Types;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models
{
    public class OrderModel : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public AddressModel Address { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<OrderItemModel> Items { get; set; }
        public int Version { get; set; }
    }
}
