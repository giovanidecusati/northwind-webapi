using NorthWind.Shared.Commands;

namespace NorthWind.Domain.Commands.Inputs.Account
{
    public class RegisterCommand : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
