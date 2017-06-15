using NorthWind.Domain.Entities;
using System.Linq;

namespace NorthWind.Infrastructure.Contexts
{
    public static class NorthWindDataContextExtensions
    {
        public static void SeedData(this NorthWindDataContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User("root", "root", "root@root.com.br", "123456", "123456"));
            }

            context.SaveChanges();
        }
    }
}
