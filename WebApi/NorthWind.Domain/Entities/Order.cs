using NorthWind.Shared.Entities;
using NorthWind.Shared.Validators;
using System.Collections.Generic;
using System.Linq;

namespace NorthWind.Domain.Entities
{
    public class Order : EntityBase
    {
        public Customer Customer { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal Total { get; private set; }
        public IList<OrderItem> OrderItens { get; private set; }

        protected Order() { }

        public Order(Customer customer)
        {
            Customer = customer;
            Status = OrderStatus.Created;
            OrderItens = new List<OrderItem>();

            new ValidationContract<Order>(this)
                .IsNotNull(Customer, "Customer is required.");
        }

        public void AddItem(Product product, int quantity)
        {
            var item = OrderItens
                .Where(p => p.Product == product)
                .FirstOrDefault();

            if (item == null)
            {
                item = new OrderItem(this, product, quantity);
                OrderItens.Add(item);
                Total += item.Total;
            }
            else
            {
                AddNotification("Product", "There is already an item with this product.");
            }
        }
    }
}