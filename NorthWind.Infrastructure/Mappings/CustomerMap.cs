using Microsoft.EntityFrameworkCore;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Mappings
{
    public static class CustomerMap
    {
        public static void CreateCustomerModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .ToTable("Customers")
                .HasKey(k => k.Id);

            modelBuilder.Entity<Customer>()
                .Ignore(p => p.Notifications);

            modelBuilder.Entity<Customer>()
                .Property(p => p.Id)
                .UseSqlServerIdentityColumn();

            modelBuilder.Entity<Customer>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
