using System;
using System.Collections.Generic;
using CleanArchitectureTemplate.Application.Commands.WriteModels;
#if(mediatr)
using MediatR;
#else
using Convey.CQRS.Commands;
#endif

namespace CleanArchitectureTemplate.Application.Commands
{
    #if(mediatr)
    public record UpdateOrder(Guid Id, Guid BuyerId, AddressWriteModel ShippingAddress, IEnumerable<OrderItemWriteModel> Items, string Status) : IRequest;
    #else
    public record UpdateOrder(Guid Id, Guid BuyerId, AddressWriteModel ShippingAddress, IEnumerable<OrderItemWriteModel> Items, string Status) : ICommand;
    #endif
}
