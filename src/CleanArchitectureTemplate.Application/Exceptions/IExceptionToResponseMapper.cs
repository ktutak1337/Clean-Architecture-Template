using System;

namespace CleanArchitectureTemplate.Application.Exceptions
{
    public interface IExceptionToResponseMapper
    {
        ExceptionResponse Map(Exception exception);
    }
}
