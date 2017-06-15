using NorthWind.Infrastructure.Contexts;

namespace NorthWind.Infrastructure.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly NorthWindDataContext _context;

        public Uow(NorthWindDataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            // Do nothing :)
        }
    }
}
