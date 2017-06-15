using NorthWind.Shared.Entities;
using NorthWind.Shared.Validators;

namespace NorthWind.Domain.Entities
{
    public class OrderItem : EntityBase
    {
        public Order Order { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal Total { get; private set; }
        public decimal UnityPrice { get; private set; }

        protected OrderItem() { }

        public OrderItem(Order order, Product product, int quantity)
        {
            Order = order;
            Product = product;
            Quantity = quantity;

            new ValidationContract<OrderItem>(this)
                .IsNotNull(Product, "Product is required.")
                .IsNotNull(Order, "Order is required.")
                .IsGreaterThan(p => p.Quantity, 0);

            if (Product != null)
            {
                UnityPrice = product.Price;
                Total = Quantity * UnityPrice;
            }
        }
    }
}