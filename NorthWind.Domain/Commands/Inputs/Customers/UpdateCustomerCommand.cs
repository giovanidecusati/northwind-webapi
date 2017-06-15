using NorthWind.Shared.Commands;
using System;

namespace NorthWind.Domain.Commands.Inputs.Customers
{
    public class UpdateCustomerCommand : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
