using AutoMapper;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;

namespace CleanArchitectureTemplate.Infrastructure.Mappings
{
    public class DocumentToDtoProfile : Profile
    {
        public DocumentToDtoProfile()
        {
            CreateMap<OrderDocument, OrderDto>();
            CreateMap<OrderItemDocument, OrderItemDto>();
            CreateMap<AddressDocument, AddressDto>();
        }
    }
}
