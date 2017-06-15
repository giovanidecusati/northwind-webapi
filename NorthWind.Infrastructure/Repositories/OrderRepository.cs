using Microsoft.EntityFrameworkCore;
using NorthWind.Domain.Entities;
using NorthWind.Domain.Repositories;
using NorthWind.Infrastructure.Contexts;
using System.Linq;

namespace NorthWind.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly NorthWindDataContext _context;
        public OrderRepository(NorthWindDataContext context)
        {
            _context = context;
        }

        public Order GetById(int id)
        {
            return _context.Orders.Where(p => p.Id == id).FirstOrDefault();
        }

        public void Create(Order entity)
        {
            _context.Orders.Add(entity);
        }

        public void Update(Order entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            _context.Orders.Remove(_context.Orders.Where(p => p.Id == id).First());
        }
    }
}
