using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AVLCarSystemApp.DataAccessInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AVLCarSystemApp.DataAccessImplementations
{
  public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
  {
    protected readonly DbContext Context;

    public Repository(DbContext context)
    {
      Context = context;
    }

    public virtual TEntity Get(long id)
    {
      return Context.Set<TEntity>().Find(id);
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
      return Context.Set<TEntity>().ToList();
    }

    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
      return Context.Set<TEntity>().Where(predicate);
    }

    public virtual void Add(TEntity entity)
    {
      Context.Set<TEntity>().Add(entity);
      Context.SaveChanges();
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
      Context.Set<TEntity>().AddRange(entities);
      Context.SaveChanges();
    }

    public virtual void Remove(TEntity entity)
    {
      Context.Set<TEntity>().Remove(entity);
      Context.SaveChanges();
    }

    public virtual bool CanRemove(TEntity entity)
    {
      return true;
    }

    //public virtual void RemoveRange(IEnumerable<TEntity> entities)
    //{
    //  Context.Set<TEntity>().RemoveRange(entities);
    //  Context.SaveChanges();
    //}

    public virtual void Update(TEntity entity)
    {
      Context.Set<TEntity>().Update(entity);
      Context.SaveChanges();
    }
  }
}
