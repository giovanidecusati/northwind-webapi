using NorthWind.Shared.Commands;

namespace NorthWind.Domain.Commands.Inputs.Account
{
    public class LoginCommand : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
