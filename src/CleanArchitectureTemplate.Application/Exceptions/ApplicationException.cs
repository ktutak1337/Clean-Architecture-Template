using System;

namespace CleanArchitectureTemplate.Application.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        protected ApplicationException(string message)
            : base(message) { }
    }
}
