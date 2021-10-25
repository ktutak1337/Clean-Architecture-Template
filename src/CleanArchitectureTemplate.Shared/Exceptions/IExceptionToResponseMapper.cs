using System;

namespace CleanArchitectureTemplate.Shared.Exceptions
{
    public interface IExceptionToResponseMapper
    {
        ExceptionResponse Map(Exception exception);
    }
}
