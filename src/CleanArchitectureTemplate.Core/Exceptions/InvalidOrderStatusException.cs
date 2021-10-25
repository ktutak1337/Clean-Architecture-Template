#if (shared)
using CleanArchitectureTemplate.Shared.Exceptions;
#endif

namespace CleanArchitectureTemplate.Core.Exceptions
{
    public class InvalidOrderStatusException : DomainException
    {
        public string Status { get; }

        public InvalidOrderStatusException(string status)
            : base($"Order status is invalid: '{status}'.")
                => Status = status;
    }
}
