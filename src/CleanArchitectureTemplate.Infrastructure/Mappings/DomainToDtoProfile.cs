using AutoMapper;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Core.Aggregates;
using CleanArchitectureTemplate.Core.Entities;
using CleanArchitectureTemplate.Core.ValueObjects;

namespace CleanArchitectureTemplate.Infrastructure.Mappings
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Address, AddressDto>();
        }
    }
}