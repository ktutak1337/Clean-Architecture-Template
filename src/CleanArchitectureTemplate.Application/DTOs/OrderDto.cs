using System;

namespace CleanArchitectureTemplate.Application.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; private set; }
        public Guid BuyerId { get; private set; }
        public AddressDto Address { get; private set; }
        public string Status { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
