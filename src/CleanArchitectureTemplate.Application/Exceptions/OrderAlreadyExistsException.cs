using System;
#if (shared)
using ApplicationException = CleanArchitectureTemplate.Shared.Exceptions.ApplicationException;
#endif

namespace CleanArchitectureTemplate.Application.Exceptions
{
    public class OrderAlreadyExistsException : ApplicationException
    {
        public Guid OrderId { get; }

        public OrderAlreadyExistsException(Guid orderId)
            : base($"Order with Id: {orderId} already exists.")
                => OrderId = orderId;
    }
}
