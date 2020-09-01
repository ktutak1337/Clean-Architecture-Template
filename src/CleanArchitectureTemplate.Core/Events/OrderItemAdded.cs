using CleanArchitectureTemplate.Core.BuildingBlocks;
using CleanArchitectureTemplate.Core.Entities;

namespace CleanArchitectureTemplate.Core.Events
{
    public class OrderItemAdded : IDomainEvent
    {
        public OrderItem OrderItem { get; }

        public OrderItemAdded(OrderItem orderItem) 
            => OrderItem = orderItem;
    }
}
