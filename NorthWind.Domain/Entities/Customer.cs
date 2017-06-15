using NorthWind.Shared.Entities;
using NorthWind.Shared.Validators;

namespace NorthWind.Domain.Entities
{
    public class Customer : EntityBase
    {
        public string Name { get; private set; }

        protected Customer() { }

        public Customer(string name)
        {
            Name = name;

            new ValidationContract<Customer>(this)
                .IsRequired(p => p.Name);
        }

        public void Change(string name)
        {
            Name = name;
            new ValidationContract<Customer>(this)
                .IsRequired(p => p.Name);
        }
    }
}