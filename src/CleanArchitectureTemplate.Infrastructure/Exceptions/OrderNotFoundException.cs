using System;
#if (!shared)
using CleanArchitectureTemplate.Infrastructure.Exceptions.Definition;
#else
using CleanArchitectureTemplate.Shared.Exceptions;
#endif

namespace CleanArchitectureTemplate.Infrastructure.Exceptions
{
    public class OrderNotFoundException : InfrastructureException
    {
        public override string Code => "order_not_found";
        public Guid OrderId { get; }

        public OrderNotFoundException(Guid orderId)
            : base($"Order with ID: {orderId} not found.")
                => OrderId = orderId;
    }
}
