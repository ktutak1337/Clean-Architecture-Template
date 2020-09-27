using System;

namespace CleanArchitectureTemplate.Application.Commands.WriteModels
{
    public class OrderItemWriteModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
    }
}
