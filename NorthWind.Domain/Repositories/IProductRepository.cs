using NorthWind.Domain.Commands.Results.Products;
using NorthWind.Domain.Entities;
using System.Collections.Generic;

namespace NorthWind.Domain.Repositories
{
    public interface IProductRepository
    {
        Product GetById(int id);
        void Create(Product entity);
        void Update(Product entity);
        void Delete(int id);
        IEnumerable<ProductsForSalesResult> GetProductsForSales();
    }
}
