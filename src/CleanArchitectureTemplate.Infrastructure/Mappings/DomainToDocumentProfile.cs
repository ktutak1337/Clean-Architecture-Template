using AutoMapper;
using CleanArchitectureTemplate.Core.Aggregates;
using CleanArchitectureTemplate.Core.Entities;
using CleanArchitectureTemplate.Core.ValueObjects;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;

namespace CleanArchitectureTemplate.Infrastructure.Mappings
{
    public class DomainToDocumentProfile : Profile
    {
        public DomainToDocumentProfile()
        {
            CreateMap<Order, OrderDocument>();
            CreateMap<OrderItem, OrderItemDocument>();
            CreateMap<Address, AddressDocument>();
        }
    }
}
