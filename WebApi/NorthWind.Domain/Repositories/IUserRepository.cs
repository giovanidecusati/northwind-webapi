using NorthWind.Domain.Entities;

namespace NorthWind.Domain.Repositories
{
    public interface IUserRepository
    {
        User GetByEmail(string email);
        void Create(User entity);
    }
}
