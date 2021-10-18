using System;
#if (shared)
using CleanArchitectureTemplate.Shared.Kernel.BuildingBlocks;
#else
using CleanArchitectureTemplate.Core.BuildingBlocks;
#endif

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
