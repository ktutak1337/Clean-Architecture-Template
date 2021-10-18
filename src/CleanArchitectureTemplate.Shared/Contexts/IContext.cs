using System;

namespace CleanArchitectureTemplate.Shared.Contexts
{
    public interface IContext
    {
        Guid RequestId { get; }
        Guid CorrelationId { get; }
        string TraceId { get; }
        string IpAddress { get; }
        string UserAgent { get; }
        IIdentityContext Identity { get; }
    }
}
