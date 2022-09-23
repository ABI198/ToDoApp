using ABISoft.ToDoAppNTier.DataAccess.Contexts;
using ABISoft.ToDoAppNTier.DataAccess.Interfaces;
using ABISoft.ToDoAppNTier.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ABISoft.ToDoAppNTier.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ToDoContext _toDoContext;
        public Repository(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }
        public async Task AddAsync(T entity)
        {
            await _toDoContext.Set<T>().AddAsync(entity);
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _toDoContext.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false)
        {
            return asNoTracking ? await _toDoContext.Set<T>().FirstOrDefaultAsync(filter)
                                : await _toDoContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(filter);
        }
        public async Task<T> GetByIdAsync(object id)
        {
            return await _toDoContext.Set<T>().FindAsync(id);
        }
       
        public void Remove(T entity)
        {
            _toDoContext.Set<T>().Remove(entity);
        }
        public void Update(T unchanged, T entity)
        {
            _toDoContext.Entry(unchanged).CurrentValues.SetValues(entity); //Optimized Update Operation
        }
        public IQueryable<T> GetQueryable()
        {
            return _toDoContext.Set<T>().AsQueryable();
        }
    }
}
