using System;
using CleanArchitectureTemplate.Application.Exceptions;

namespace CleanArchitectureTemplate.Infrastructure.Exceptions.Definition
{
    public interface IExceptionCompositionRoot
    {
        ExceptionResponse Map(Exception exception);
    }
}
