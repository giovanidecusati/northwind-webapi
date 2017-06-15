using NorthWind.Shared.Commands;

namespace NorthWind.Domain.Commands.Inputs.Orders
{
    public class RegisterOrderItemCommand : ICommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
