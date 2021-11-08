using System;
using CleanArchitectureTemplate.Application.DTOs;
#if(mediatr)
using MediatR;
#else
using Convey.CQRS.Queries;
#endif

namespace CleanArchitectureTemplate.Application.Queries
{
    #if(mediatr)
    public class GetOrder : IRequest<OrderDto>
    {
        public Guid Id { get; set; }
    }
    #else
    public class GetOrder : IQuery<OrderDto>
    {
        public Guid Id { get; set; }
    }
    #endif
}
