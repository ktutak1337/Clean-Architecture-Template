using System.Collections.Generic;
using CleanArchitectureTemplate.Application.DTOs;
#if(mediatr)
using MediatR;
#else
using Convey.CQRS.Queries;
#endif

namespace CleanArchitectureTemplate.Application.Queries
{
    #if(mediatr)
    public class GetOrders : IRequest<IEnumerable<OrderDto>> { }
    #else
    public class GetOrders : IQuery<IEnumerable<OrderDto>> { }
    #endif
}
