using System;

namespace CleanArchitectureTemplate.Shared.Exceptions
{
    public interface IExceptionCompositionRoot
    {
        ExceptionResponse Map(Exception exception);
    }
}
