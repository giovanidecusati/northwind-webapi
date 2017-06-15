using NorthWind.Shared.Entities;
using NorthWind.Shared.Validators;

namespace NorthWind.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        protected Product() { }

        public Product(string name, decimal price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;

            new ValidationContract<Product>(this)
                .IsRequired(p => p.Name)
                .HasMaxLenght(p => p.Name, 256)
                .IsGreaterThan(p => p.Price, 0.01M)
                .IsGreaterOrEqualsThan(p => p.Stock, 0);
        }

        public void Change(string name, decimal price)
        {
            Name = name;
            Price = price;

            new ValidationContract<Product>(this)
                .IsRequired(p => p.Name)
                .HasMaxLenght(p => p.Name, 256)
                .IsGreaterThan(p => p.Price, 0.01M);
        }

        public void DecreaseStockLevel(int quantity)
        {
            new ValidationContract<Product>(this)
                .IsGreaterThan(p => p.Stock, quantity);

            Stock -= quantity;
        }

        public void EncreaseStockLevel(int quantity)
        {
            Stock += quantity;
        }
    }
}