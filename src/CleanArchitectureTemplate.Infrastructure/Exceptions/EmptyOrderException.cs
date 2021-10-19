#if (!shared)
using CleanArchitectureTemplate.Infrastructure.Exceptions.Definition;
#else
using CleanArchitectureTemplate.Shared.Exceptions;
#endif

namespace CleanArchitectureTemplate.Infrastructure.Exceptions
{
    public class EmptyOrderException : InfrastructureException
    {
        public EmptyOrderException()
            : base($"Empty order defined.") { }
    }
}
