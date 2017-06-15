using Microsoft.EntityFrameworkCore;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Mappings
{
    public static class UserMap
    {
        public static void CreateUserModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users")            
                .HasKey(k => k.Id);

            modelBuilder.Entity<User>()
                .Ignore(p => p.Notifications);

            modelBuilder.Entity<User>()
                .HasAlternateKey(p => p.Email);

            modelBuilder.Entity<User>()
                .Property(p => p.Id)
                .IsRequired()
                .UseSqlServerIdentityColumn();

            modelBuilder.Entity<User>()
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(256);

            modelBuilder.Entity<User>()
                .Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(256);

            modelBuilder.Entity<User>()
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(256);

            modelBuilder.Entity<User>()
                .Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
