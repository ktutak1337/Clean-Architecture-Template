#if (shared)
using CleanArchitectureTemplate.Shared.Kernel.BuildingBlocks;
#else
using CleanArchitectureTemplate.Core.BuildingBlocks;
#endif
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
