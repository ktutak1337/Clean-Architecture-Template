using System;
using System.Collections.Generic;
using CleanArchitectureTemplate.Core.Types;
#if (shared && postgres)
using CleanArchitectureTemplate.Shared.Persistence.Types;
#else
using CleanArchitectureTemplate.Infrastructure.Persistence.Types;
#endif

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models
{
    public class OrderModel : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public AddressModel ShippingAddress { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<OrderItemModel> Items { get; set; }
        public int Version { get; set; }
    }
}
