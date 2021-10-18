using System;

namespace CleanArchitectureTemplate.Shared.Exceptions
{
    public class InvalidTypedIdException : DomainException
    {
        public override string Code => "invalid_typed_id";
        public Guid Id { get; }
        public InvalidTypedIdException(Guid id)
            : base($"Invalid typed ID: '{id}'. Id value cannot be null or empty!")
                => Id = id;
    }
}
