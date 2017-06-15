using NorthWind.Domain.Commands.Inputs.Account;
using NorthWind.Domain.Entities;
using NorthWind.Domain.Repositories;
using NorthWind.Shared.Commands;
using NorthWind.Shared.Notifications;

namespace NorthWind.Domain.Commands.Handlers
{
    public class AccountHandler : Notifiable,
        ICommandHandler<RegisterCommand, ICommandResult>
    {
        readonly IUserRepository _userRepository;

        public AccountHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Handle(RegisterCommand command)
        {
            if (_userRepository.GetByEmail(command.Email) == null)
            {
                var user = new User(command.FirstName, command.LastName, command.Email, command.Password, command.ConfirmPassword);
                if (!user.IsValid())
                {
                    AddNotifications(user.Notifications);
                    return null;
                }

                _userRepository.Create(user);

                return new CreatedCommandResult(user);
            }
            else
            {
                AddNotification("User", "There is a user with this e-mail.");
                return null;
            }
        }
    }
}
