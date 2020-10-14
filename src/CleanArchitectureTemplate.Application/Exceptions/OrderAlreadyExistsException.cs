using System;
#if (shared)
using ApplicationException = CleanArchitectureTemplate.Shared.Kernel.Exceptions.ApplicationException;
#endif

namespace CleanArchitectureTemplate.Application.Exceptions
{
    public class OrderAlreadyExistsException : ApplicationException
    {
        public override string Code => "order_already_exists";
        public Guid OrderId { get; }

        public OrderAlreadyExistsException(Guid orderId) 
            : base($"Order with Id: {orderId} already exists.")
                => OrderId = orderId;
    }
}
