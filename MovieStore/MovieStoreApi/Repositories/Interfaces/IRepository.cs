using System.Linq.Expressions;

namespace MovieStoreApi.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    void Add(T entity);
    void Delete(T entity);
    IEnumerable<T> GetAll();
    T? GetById(Guid id);
    void SaveChanges();
    IEnumerable<T> Search(Expression<Func<T,bool>> predicate);
}