using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Infrastructure;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Repositories;

public abstract class GenericRepository<T>:IRepository<T> where T : class
{
    
    protected readonly MovieStoreContext _context;

    protected GenericRepository(MovieStoreContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void Edit(T entity)
    {
        _context.Entry<T>(entity).State = EntityState.Modified;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T? GetById(Guid id)
    {
        return _context.Set<T>().Find(id);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
    public IEnumerable<T> Search(Expression<Func<T,bool>> predicate) {
        return _context.Set<T>().Where(predicate).AsEnumerable();
    }
}