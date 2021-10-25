using System;
using System.Collections.Generic;

namespace CleanArchitectureTemplate.Application.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public string Status { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
