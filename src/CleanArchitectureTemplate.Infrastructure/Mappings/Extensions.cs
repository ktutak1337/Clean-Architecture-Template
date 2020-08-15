using System.Linq;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Core.Aggregates;
using CleanArchitectureTemplate.Core.Entities;
using CleanArchitectureTemplate.Core.ValueObjects;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;

namespace CleanArchitectureTemplate.Infrastructure.Mappings
{
    public static class Extensions
    {
        public static OrderDocument AsDocument(this Order order)
            => new OrderDocument
            {
                Id = order.Id,
                BuyerId = order.BuyerId,
                Address = new AddressDocument
                {
                    City = order.Address.City,
                    Street = order.Address.Street,
                    Province = order.Address.Province,
                    Country = order.Address.Country,
                    ZipCode = order.Address.ZipCode
                },
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                CreatedAt = order.CreatedAt,
                Version = order.Version,
                Items = order.Items.Select(item => new OrderItemDocument
                {
                    Id = item.Id,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Price = item.Price
                })
            };

            public static Order AsEntity(this OrderDocument document)
                => new Order(
                    document.Id,
                    document.BuyerId,
                    new Address(
                        document.Address.City,
                        document.Address.Street,
                        document.Address.Province,
                        document.Address.Country, 
                        document.Address.ZipCode),
                    document.Items.Select(item => new OrderItem(item.Name, item.Quantity, item.UnitPrice)),
                    document.Status,
                    document.Version);

            public static OrderDto AsDto(this OrderDocument document)
                => new OrderDto
                {
                    Id = document.Id,
                    BuyerId = document.BuyerId,
                    Address = new AddressDto
                    {
                        City = document.Address.City,
                        Street = document.Address.Street,
                        Province = document.Address.Province,
                        Country = document.Address.Country,
                        ZipCode = document.Address.ZipCode
                    },
                    Status = document.Status.ToString().ToLowerInvariant(),
                    TotalPrice = document.TotalPrice,
                    CreatedAt = document.CreatedAt, 
                    Items = document.Items.Select(item => new OrderItemDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        Price = item.Price
                    })
                };
    }
}
