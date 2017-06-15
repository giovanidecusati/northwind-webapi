using Microsoft.EntityFrameworkCore;
using NorthWind.Domain.Entities;
using NorthWind.Domain.Repositories;
using NorthWind.Infrastructure.Contexts;
using System.Linq;
using NorthWind.Domain.Commands.Results.Products;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using NorthWind.Shared;

namespace NorthWind.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly NorthWindDataContext _context;
        public ProductRepository(NorthWindDataContext context)
        {
            _context = context;
        }

        public Product GetById(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public void Create(Product entity)
        {
            _context.Products.Add(entity);
        }

        public void Update(Product entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            _context.Products.Remove(_context.Products.Where(p => p.Id == id).First());
        }

        public IEnumerable<ProductsForSalesResult> GetProductsForSales()
        {
            string query =
                @"SELECT 
                        [Id],
                        [Name]
                    FROM [Products]
                    WHERE [Stock] > 0";

            using (var connection = new SqlConnection(Runtime.ConnectionString))
            {
                return connection.Query<ProductsForSalesResult>(query);
            }
        }
    }
}
