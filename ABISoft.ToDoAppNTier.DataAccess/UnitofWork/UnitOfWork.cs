using ABISoft.ToDoAppNTier.DataAccess.Contexts;
using ABISoft.ToDoAppNTier.DataAccess.Interfaces;
using ABISoft.ToDoAppNTier.DataAccess.Repositories;
using ABISoft.ToDoAppNTier.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABISoft.ToDoAppNTier.DataAccess.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoContext _toDoContext;
        public UnitOfWork(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_toDoContext);
        }
        public async Task SaveChangesAsync()
        {
            await _toDoContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _toDoContext.Dispose();
        }
    }
}
