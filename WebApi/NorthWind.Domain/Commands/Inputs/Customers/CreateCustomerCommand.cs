using NorthWind.Shared.Commands;

namespace NorthWind.Domain.Commands.Inputs.Customers
{
    public class CreateCustomerCommand : ICommand
    {
        public string Name { get; set; }
    }
}
