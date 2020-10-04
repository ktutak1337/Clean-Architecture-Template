using System;
using CleanArchitectureTemplate.Core.BuildingBlocks;

namespace CleanArchitectureTemplate.Core
{
    public sealed class OrderId : TypedIdValueBase
    {
        public OrderId(Guid value) 
            : base(value) { }

        public static implicit operator OrderId(Guid orderId)
            => new OrderId(orderId);
    }
}
