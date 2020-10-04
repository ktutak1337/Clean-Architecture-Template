using System.Collections.Generic;
using CleanArchitectureTemplate.Application.DTOs;
using Convey.CQRS.Queries;

namespace CleanArchitectureTemplate.Application.Queries
{
    public class GetOrders : IQuery<IEnumerable<OrderDto>> { }
}
