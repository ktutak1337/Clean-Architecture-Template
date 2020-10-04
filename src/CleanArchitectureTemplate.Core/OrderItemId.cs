using System;
using CleanArchitectureTemplate.Core.BuildingBlocks;

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
