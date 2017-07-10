using NorthWind.Domain.Commands.Inputs.Orders;
using NorthWind.Domain.Entities;
using NorthWind.Domain.Repositories;
using NorthWind.Shared.Commands;
using NorthWind.Shared.Notifications;

namespace NorthWind.Domain.Commands.Handlers
{
    public class OrderHandler : Notifiable,
        ICommandHandler<RegisterOrderCommand, ICommandResult>
    {
        readonly IOrderRepository _orderRepository;
        readonly IProductRepository _productRepository;
        readonly ICustomerRepository _customerRepository;

        public OrderHandler(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public ICommandResult Handle(RegisterOrderCommand command)
        {
            var customer = _customerRepository.GetById(command.CustomerId);

            var order = new Order(customer);

            foreach (var item in command.Itens)
            {
                var product = _productRepository.GetById(item.ProductId);
                order.AddItem(product, item.Quantity);
            }

            AddNotifications(order.Notifications);

            if (order.IsValid())
            {
                _orderRepository.Create(order);
                return new CreatedCommandResult(order);
            }

            return null;
        }
    }
}