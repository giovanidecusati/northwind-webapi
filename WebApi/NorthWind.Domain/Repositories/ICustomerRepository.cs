using NorthWind.Domain.Entities;

namespace NorthWind.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetById(int id);
        void Create(Customer entity);
        void Update(Customer entity);
        void Delete(int id);
    }
}
