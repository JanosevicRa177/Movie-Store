using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Infrastructure;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Repositories;

public abstract class GenericRepository<T>:IRepository<T> where T : class
{
    
    protected readonly MovieStoreContext Context;

    protected GenericRepository(MovieStoreContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    public IEnumerable<T> GetAll()
    {
        return Context.Set<T>().ToList();
    }

    public virtual T? GetById(Guid id)
    {
        return Context.Set<T>().Find(id);
    }

    public void SaveChanges()
    {
        Context.SaveChanges();
    }
    public virtual IEnumerable<T> Search(Expression<Func<T,bool>> predicate) {
        return Context.Set<T>().Where(predicate);
    }
}