using System;
using CleanArchitectureTemplate.Core.BuildingBlocks;

namespace CleanArchitectureTemplate.Core
{
    public class BuyerId : TypedIdValueBase
    {
        protected BuyerId(Guid value) : base(value)
        {

        }

        public static implicit operator BuyerId(Guid buyerId)
            => new BuyerId(buyerId);
    }
}
