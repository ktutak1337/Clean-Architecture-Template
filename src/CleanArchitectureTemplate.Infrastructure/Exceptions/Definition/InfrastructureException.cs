using System;

namespace CleanArchitectureTemplate.Infrastructure.Exceptions.Definition
{
    public abstract class InfrastructureException : Exception
    {
        public virtual string Code { get; }

        protected InfrastructureException(string message)
            : base(message) { }
    }
}
