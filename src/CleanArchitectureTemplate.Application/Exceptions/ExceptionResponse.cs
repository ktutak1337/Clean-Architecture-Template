using System.Net;

namespace CleanArchitectureTemplate.Application.Exceptions
{
    public record ExceptionResponse(object Response, HttpStatusCode StatusCode);
}
