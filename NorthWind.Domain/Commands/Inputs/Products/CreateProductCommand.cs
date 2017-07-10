using NorthWind.Shared.Commands;

namespace NorthWind.Domain.Commands.Inputs.Products
{
    public class CreateProductCommand : ICommand
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
