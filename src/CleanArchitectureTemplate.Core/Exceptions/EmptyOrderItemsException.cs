using System;
#if (shared)
using CleanArchitectureTemplate.Shared.Kernel.Exceptions;
#endif

namespace CleanArchitectureTemplate.Core.Exceptions
{
    public class EmptyOrderItemsException : DomainException
    {
        public override string Code => "empty_order_items";
        public Guid OrderId { get; }
        
        public EmptyOrderItemsException(Guid orderId) 
            : base($"Empty order items defined for order with ID: {orderId}")
                => OrderId = orderId;
    }
}
