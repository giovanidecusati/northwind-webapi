using NorthWind.Domain.Entities;

namespace NorthWind.Domain.Repositories
{
    public interface IOrderRepository
    {
        Order GetById(int id);
        void Create(Order entity);
        void Update(Order entity);
        void Delete(int id);
    }
}
