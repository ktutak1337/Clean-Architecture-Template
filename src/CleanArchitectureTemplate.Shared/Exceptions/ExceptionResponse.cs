using System.Net;

namespace CleanArchitectureTemplate.Shared.Exceptions
{
    public record ExceptionResponse(object Response, HttpStatusCode StatusCode);
}
