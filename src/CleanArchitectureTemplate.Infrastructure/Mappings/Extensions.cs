using System.Linq;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Core.Aggregates;
#if (mongo || postgres)
using CleanArchitectureTemplate.Core.Entities;
using CleanArchitectureTemplate.Core.ValueObjects;
#endif
#if (postgres)
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models;
#endif
#if (mongo)
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;
#endif

namespace CleanArchitectureTemplate.Infrastructure.Mappings
{
    public static class Extensions
    {
        #if (mongo)
        public static OrderDocument AsDocument(this Order order)
            => new OrderDocument
            {
                Id = order.Id,
                BuyerId = order.BuyerId,
                ShippingAddress = new AddressDocument
                {
                    City = order.ShippingAddress.City,
                    Street = order.ShippingAddress.Street,
                    Province = order.ShippingAddress.Province,
                    Country = order.ShippingAddress.Country,
                    ZipCode = order.ShippingAddress.ZipCode
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
                    document.ShippingAddress.City,
                    document.ShippingAddress.Street,
                    document.ShippingAddress.Province,
                    document.ShippingAddress.Country,
                    document.ShippingAddress.ZipCode),
                document.Items.Select(item => new OrderItem(item.Name, item.Quantity, item.UnitPrice)),
                document.Status,
                document.Version);

        public static OrderDto AsDto(this OrderDocument document)
            => new OrderDto
            {
                Id = document.Id,
                BuyerId = document.BuyerId,
                ShippingAddress = new AddressDto
                {
                    City = document.ShippingAddress.City,
                    Street = document.ShippingAddress.Street,
                    Province = document.ShippingAddress.Province,
                    Country = document.ShippingAddress.Country,
                    ZipCode = document.ShippingAddress.ZipCode
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
        #endif
        #if (postgres)
        public static OrderModel AsDatabaseModel(this Order order)
            => new OrderModel
            {
                Id = order.Id,
                BuyerId = order.BuyerId,
                ShippingAddress = new AddressModel
                {
                    City = order.ShippingAddress.City,
                    Street = order.ShippingAddress.Street,
                    Province = order.ShippingAddress.Province,
                    Country = order.ShippingAddress.Country,
                    ZipCode = order.ShippingAddress.ZipCode
                },
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                CreatedAt = order.CreatedAt,
                Version = order.Version,
                Items = order.Items.Select(item => new OrderItemModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Price = item.Price
                })
            };

        public static Order AsEntity(this OrderModel model)
            => new Order(
                model.Id,
                model.BuyerId,
                new Address(
                    model.ShippingAddress.City,
                    model.ShippingAddress.Street,
                    model.ShippingAddress.Province,
                    model.ShippingAddress.Country,
                    model.ShippingAddress.ZipCode),
                model.Items.Select(item => new OrderItem(item.Name, item.Quantity, item.UnitPrice)),
                model.Status,
                model.Version);

        public static OrderDto AsDto(this OrderModel model)
            => new OrderDto
            {
                Id = model.Id,
                BuyerId = model.BuyerId,
                ShippingAddress = new AddressDto
                {
                    City = model.ShippingAddress.City,
                    Street = model.ShippingAddress.Street,
                    Province = model.ShippingAddress.Province,
                    Country = model.ShippingAddress.Country,
                    ZipCode = model.ShippingAddress.ZipCode
                },
                Status = model.Status.ToString().ToLowerInvariant(),
                TotalPrice = model.TotalPrice,
                CreatedAt = model.CreatedAt,
                Items = model.Items.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Price = item.Price
                })
            };
        #endif
        #if (!mongo && !postgres)
        public static OrderDto AsDto(this Order order)
            => new OrderDto
            {
                Id = order.Id,
                BuyerId = order.BuyerId,
                ShippingAddress = new AddressDto
                {
                    City = order.ShippingAddress.City,
                    Street = order.ShippingAddress.Street,
                    Province = order.ShippingAddress.Province,
                    Country = order.ShippingAddress.Country,
                    ZipCode = order.ShippingAddress.ZipCode
                },
                Status = order.Status.ToString().ToLowerInvariant(),
                TotalPrice = order.TotalPrice,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Price = item.Price
                })
            };
        #endif
    }
}
