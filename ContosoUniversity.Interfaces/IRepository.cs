using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Read
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        // Add
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        // Remove
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        // no update
    }
}
