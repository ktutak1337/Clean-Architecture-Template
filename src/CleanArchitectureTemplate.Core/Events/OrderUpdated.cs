using CleanArchitectureTemplate.Core.Aggregates;
#if (shared)
using CleanArchitectureTemplate.Shared.BuildingBlocks;
#else
using CleanArchitectureTemplate.Core.BuildingBlocks;
#endif

namespace CleanArchitectureTemplate.Core.Events
{
    public class OrderUpdated : IDomainEvent
    {
        public Order Order { get; }

        public OrderUpdated(Order order) 
            => Order = order;
    }
}
