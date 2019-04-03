using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AVLCarSystemApp.DataAccessInterfaces
{
  public interface IRepository<TEntity> where TEntity : class
  {
    TEntity Get(long id);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    bool CanRemove(TEntity entity);
    //void RemoveRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
  }
}
