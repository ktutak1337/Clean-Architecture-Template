using System;
#if (shared)
using CleanArchitectureTemplate.Shared.BuildingBlocks;
#else
using CleanArchitectureTemplate.Core.BuildingBlocks;
#endif

namespace CleanArchitectureTemplate.Core
{
    public sealed class OrderItemId : TypedIdValueBase
    {
        public OrderItemId(Guid value) 
            : base(value) { }

        public static implicit operator OrderItemId(Guid orderItemId)
            => new OrderItemId(orderItemId);
    }
}
