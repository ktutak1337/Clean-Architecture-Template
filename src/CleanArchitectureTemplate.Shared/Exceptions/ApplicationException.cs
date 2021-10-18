using System;

namespace CleanArchitectureTemplate.Shared.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        public virtual string Code { get; }

        protected ApplicationException(string message)
            : base(message) { }
    }
}
