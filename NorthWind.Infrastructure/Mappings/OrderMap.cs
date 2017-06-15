using Microsoft.EntityFrameworkCore;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Mappings
{
    public static class OrderMap
    {
        public static void CreateOrderModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .ToTable("Orders")
                .HasKey(k => k.Id);

            modelBuilder.Entity<Order>()
                .Ignore(p => p.Notifications);

            modelBuilder.Entity<Order>()
                .Property(p => p.Id)
                .UseSqlServerIdentityColumn();

            modelBuilder.Entity<Order>()
            .HasOne(p => p.Customer);

            modelBuilder.Entity<Order>()
                .HasMany(p => p.OrderItens)
                .WithOne(p => p.Order);

            modelBuilder.Entity<Order>()
                .Property(p => p.Status);

            modelBuilder.Entity<Order>()
                .Property(p => p.Total)
                .IsRequired();
        }
    }
}
