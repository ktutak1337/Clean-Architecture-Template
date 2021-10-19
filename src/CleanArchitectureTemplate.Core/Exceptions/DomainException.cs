using System;

namespace CleanArchitectureTemplate.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message)
            : base(message) { }
    }
}
