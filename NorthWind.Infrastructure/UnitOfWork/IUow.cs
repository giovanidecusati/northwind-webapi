using System;
using System.Collections.Generic;
using System.Text;

namespace NorthWind.Infrastructure.UnitOfWork
{
    public interface IUow
    {
        void Commit();
        void Rollback();
    }
}
