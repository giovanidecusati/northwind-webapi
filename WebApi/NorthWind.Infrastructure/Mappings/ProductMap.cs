using Microsoft.EntityFrameworkCore;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Mappings
{
    public static class ProductMap
    {
        public static void CreateProductModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .HasKey(k => k.Id);

            modelBuilder.Entity<Product>()
                .Ignore(p => p.Notifications);

            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .UseSqlServerIdentityColumn();

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(256);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Stock)
                .IsRequired();
        }
    }
}
