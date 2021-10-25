using System;

namespace CleanArchitectureTemplate.Infrastructure.Exceptions.Definition
{
    public abstract class InfrastructureException : Exception
    {
        protected InfrastructureException(string message)
            : base(message) { }
    }
}
