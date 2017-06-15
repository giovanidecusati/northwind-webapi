using NorthWind.Domain.Commands.Inputs.Customers;
using NorthWind.Domain.Entities;
using NorthWind.Domain.Repositories;
using NorthWind.Shared.Commands;
using NorthWind.Shared.Notifications;

namespace NorthWind.Domain.Commands.Handlers
{
    public class CustomerHandler : Notifiable,
        ICommandHandler<CreateCustomerCommand, ICommandResult>,
        ICommandHandler<UpdateCustomerCommand, ICommandResult>
    {
        readonly ICustomerRepository _customerRepository;

        public CustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ICommandResult Handle(CreateCustomerCommand command)
        {
            var customer = new Customer(command.Name);
            if (!customer.IsValid())
            {
                AddNotifications(customer.Notifications);
                return null;
            }

            _customerRepository.Create(customer);

            return new CreatedCommandResult(customer);
        }

        public ICommandResult Handle(UpdateCustomerCommand command)
        {
            var customer = _customerRepository.GetById(command.Id);

            customer.Change(command.Name);
            if (!customer.IsValid())
            {
                AddNotifications(customer.Notifications);
                return null;
            }

            _customerRepository.Update(customer);

            return null;
        }

        public ICommandResult Handle(int id)
        {
            _customerRepository.Delete(id);

            return null;
        }
    }
}
