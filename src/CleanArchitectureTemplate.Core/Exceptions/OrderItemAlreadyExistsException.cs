using System;
#if (shared)
using CleanArchitectureTemplate.Shared.Exceptions;
#endif

namespace CleanArchitectureTemplate.Core.Exceptions
{
    public class OrderItemAlreadyExistsException : DomainException
    {
        public override string Code => "order_item_already_exists";
        public Guid OrderItemId { get; }

        public OrderItemAlreadyExistsException(Guid orderItemId)
            : base($"Order item with ID: {orderItemId} already exists.")
                => OrderItemId = orderItemId;
    }
}
