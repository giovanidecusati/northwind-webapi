using NorthWind.Domain.Commands.Inputs.Products;
using NorthWind.Domain.Entities;
using NorthWind.Domain.Repositories;
using NorthWind.Shared.Commands;
using NorthWind.Shared.Notifications;

namespace NorthWind.Domain.Commands.Handlers
{
    public class ProductHandler : Notifiable,
        ICommandHandler<CreateProductCommand, ICommandResult>,
        ICommandHandler<UpdateProductCommand, ICommandResult>
    {
        readonly IProductRepository _productRepository;

        public ProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ICommandResult Handle(CreateProductCommand command)
        {
            var product = new Product(command.Name, command.Price, command.Stock);
            AddNotifications(product.Notifications);

            if (product.IsValid())
            {
                _productRepository.Create(product);
                return new CreatedCommandResult(product);
            }

            return null;
        }

        public ICommandResult Handle(UpdateProductCommand command)
        {
            var product = _productRepository.GetById(command.Id);
            product.Change(command.Name, command.Price);
            AddNotifications(product.Notifications);

            if (product.IsValid())            
                _productRepository.Update(product);

            return null;
        }

        public ICommandResult Handle(int id)
        {
            _productRepository.Delete(id);

            return null;
        }
    }
}
