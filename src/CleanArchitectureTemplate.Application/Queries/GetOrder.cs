using System;
using CleanArchitectureTemplate.Application.DTOs;
using Convey.CQRS.Queries;

namespace CleanArchitectureTemplate.Application.Queries
{
    public class GetOrder : IQuery<OrderDto>
    {
        public Guid Id { get; set; }
    }
}
