using System;
#if (shared)
using CleanArchitectureTemplate.Shared.BuildingBlocks;
#else
using CleanArchitectureTemplate.Core.BuildingBlocks;
#endif

namespace CleanArchitectureTemplate.Core
{
    public class BuyerId : TypedIdValueBase
    {
        public BuyerId(Guid value) 
            : base(value) { }

        public static implicit operator BuyerId(Guid buyerId)
            => new BuyerId(buyerId);
    }
}
