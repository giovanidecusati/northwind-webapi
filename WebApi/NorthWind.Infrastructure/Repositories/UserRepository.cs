using System;
using System.Linq;
using NorthWind.Domain.Entities;
using NorthWind.Domain.Repositories;
using NorthWind.Infrastructure.Contexts;

namespace NorthWind.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly NorthWindDataContext _context;
        public UserRepository(NorthWindDataContext context)
        {
            _context = context;
        }

        public User GetByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public void Create(User user)
        {
            _context.Add(user);
        }
    }
}
