using System;
#if (shared)
using CleanArchitectureTemplate.Shared.Kernel.BuildingBlocks;
#else
using CleanArchitectureTemplate.Core.BuildingBlocks;
using CleanArchitectureTemplate.Core.ValueObjects;
#endif

namespace CleanArchitectureTemplate.Core.Entities
{
    public class OrderItem : IEntity<OrderItemId>
    {
        public OrderItemId Id { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public Amount UnitPrice { get; private set; }
        public Amount Price { get; private set; }

        private OrderItem() { }

        public OrderItem(string name, int quantity, Amount unitPrice)
        {
            Id = new OrderItemId(Guid.NewGuid());
            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Price = quantity * unitPrice;
        }
    }
}
