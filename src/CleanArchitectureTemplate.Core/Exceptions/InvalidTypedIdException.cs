using System;

namespace CleanArchitectureTemplate.Core.Exceptions
{
    public class InvalidTypedIdException : DomainException
    {
        public Guid Id { get; }
        public InvalidTypedIdException(Guid id)
            : base($"Invalid typed ID: '{id}'. Id value cannot be null or empty!")
                => Id = id;
    }
}
