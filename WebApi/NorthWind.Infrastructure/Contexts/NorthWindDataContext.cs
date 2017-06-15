using Microsoft.EntityFrameworkCore;
using NorthWind.Domain.Entities;
using NorthWind.Infrastructure.Mappings;
using NorthWind.Shared;

namespace NorthWind.Infrastructure.Contexts
{
    public class NorthWindDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Runtime.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.CreateUserModel();

            modelBuilder.CreateProductModel();

            modelBuilder.CreateCustomerModel();

            modelBuilder.CreateOrderModel();

            modelBuilder.CreateOrderItemModel();
        }
    }
}
