using NorthWind.Shared.Commands;
using System.Collections.Generic;

namespace NorthWind.Domain.Commands.Inputs.Orders
{
    public class RegisterOrderCommand : ICommand
    {
        public int CustomerId { get; set; }
        public IEnumerable<RegisterOrderItemCommand> Itens { get; set; } = new List<RegisterOrderItemCommand>();


    }
}
