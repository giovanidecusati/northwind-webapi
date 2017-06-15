using Microsoft.EntityFrameworkCore;
using NorthWind.Domain.Entities;
using NorthWind.Domain.Repositories;
using NorthWind.Infrastructure.Contexts;
using System.Linq;

namespace NorthWind.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        readonly NorthWindDataContext _context;
        public CustomerRepository(NorthWindDataContext context)
        {
            _context = context;
        }

        public Customer GetById(int id)
        {
            return _context.Customers.Where(p => p.Id == id).FirstOrDefault();
        }

        public void Create(Customer entity)
        {
            _context.Customers.Add(entity);
        }

        public void Update(Customer entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            _context.Customers.Remove(_context.Customers.Where(p => p.Id == id).First());
        }
    }
}
