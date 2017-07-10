using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWind.Domain.Commands.Handlers;
using NorthWind.Domain.Repositories;
using Moq;
using NorthWind.Domain.Commands.Inputs.Products;
using NorthWind.Domain.Entities;
using FluentAssertions;

namespace NorthWind.Domain.UnitTest
{
    [TestClass]
    public class AccountHandlerUnitTest
    {

        [TestMethod]
        public void Can_Create_New_Product()
        {
            var moq = new Mock<IProductRepository>();
            moq.Setup(x => x.Create(It.IsAny<Product>()));

            var handler = new ProductHandler(moq.Object);

            var result = handler
                .Handle(new CreateProductCommand() { Name = "Product Name", Price = 1.99M, Stock = 999 });

            result
                .Should()
                .BeOfType<Shared.Commands.CreatedCommandResult>();

            handler.Notifications
                .Should()
                .BeEmpty();

            moq.Verify(x => x.Create(It.IsAny<Product>()), Times.Once());
        }

        [TestMethod]
        public void Can_Updade_Product()
        {
            var entity = new Product("Product Name", 1.99M, 999);

            var moq = new Mock<IProductRepository>();
            moq.Setup(x => x.Update(It.IsAny<Product>()));
            moq.Setup(x => x.GetById(It.IsAny<int>())).Returns(entity);

            var handler = new ProductHandler(moq.Object);

            var result = handler
                .Handle(new UpdateProductCommand() { Id = 0, Name = "Product Name", Price = 1.99M });

            result
                .Should()
                .BeNull();

            handler.Notifications
                .Should()
                .BeEmpty();

            moq.Verify(x => x.Update(It.IsAny<Product>()), Times.Once());
            moq.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
        }
    }
}
