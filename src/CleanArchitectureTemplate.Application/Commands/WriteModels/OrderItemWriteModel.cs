using System;

namespace CleanArchitectureTemplate.Application.Commands.WriteModels
{
    public class OrderItemWriteModel
    {
        public Guid Id { get; }
        public string Name { get; }
        public int Quantity { get; }
        public decimal UnitPrice { get; }
        public decimal Price { get; }

        public OrderItemWriteModel(Guid id, string name, int quantity, decimal unitPrice)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Price = quantity * unitPrice;
        }
    }
}
