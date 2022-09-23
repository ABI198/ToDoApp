using ABISoft.ToDoAppNTier.DataAccess.Interfaces;
using ABISoft.ToDoAppNTier.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABISoft.ToDoAppNTier.DataAccess.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        Task SaveChangesAsync();
    }
}
