using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CleanArchitectureTemplate.Application.Commands.WriteModels;
using CleanArchitectureTemplate.Core;
#if(mediatr)
using MediatR;
#else
using Convey.CQRS.Commands;
#endif

namespace CleanArchitectureTemplate.Application.Commands
{
    #if(mediatr)
    public record CreateOrder([Required] Guid BuyerId, [Required] AddressWriteModel ShippingAddress, [Required] IEnumerable<OrderItemWriteModel> Items) : IRequest
    {
        public Guid Id { get; init; } = new OrderId(Guid.NewGuid());
    }
    #else
    public record CreateOrder([Required] Guid BuyerId, [Required] AddressWriteModel ShippingAddress, [Required] IEnumerable<OrderItemWriteModel> Items) : ICommand
    {
        public Guid Id { get; init; } = new OrderId(Guid.NewGuid());
    }
    #endif
}
