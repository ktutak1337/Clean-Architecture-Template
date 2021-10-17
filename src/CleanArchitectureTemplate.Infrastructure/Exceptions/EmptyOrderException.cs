#if (!shared)
using CleanArchitectureTemplate.Infrastructure.Exceptions.Definition;
#else
using CleanArchitectureTemplate.Shared.Kernel.Exceptions;
#endif

namespace CleanArchitectureTemplate.Infrastructure.Exceptions
{
    public class EmptyOrderException : InfrastructureException
    {
        public override string Code => "empty_order";

        public EmptyOrderException()
            : base($"Empty order defined.") { }
    }
}
