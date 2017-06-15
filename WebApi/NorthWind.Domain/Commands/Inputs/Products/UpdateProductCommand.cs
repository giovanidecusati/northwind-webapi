using NorthWind.Shared.Commands;

namespace NorthWind.Domain.Commands.Inputs.Products
{
    public class UpdateProductCommand : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
