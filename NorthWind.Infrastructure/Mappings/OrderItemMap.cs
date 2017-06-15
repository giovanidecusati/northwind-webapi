using Microsoft.EntityFrameworkCore;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Mappings
{
    public static class OrderItemMap
    {
        public static void CreateOrderItemModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .ToTable("OrderItens")
                .HasKey(k => k.Id);

            modelBuilder.Entity<OrderItem>()
                .Ignore(p => p.Notifications);

            modelBuilder.Entity<OrderItem>()
                .Property(p => p.Id)
                .UseSqlServerIdentityColumn();

            modelBuilder.Entity<OrderItem>()
                .HasOne(p => p.Order);

            modelBuilder.Entity<OrderItem>()
                .HasOne(p => p.Product);

            modelBuilder.Entity<OrderItem>()
                .Property(p => p.Quantity)
                .IsRequired();

            modelBuilder.Entity<OrderItem>()
                .Property(p => p.Total)
                .IsRequired();

            modelBuilder.Entity<OrderItem>()
                .Property(p => p.UnityPrice)
                .IsRequired();
        }
    }
}
