using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Model;
using MovieStore.Infrastructure;

namespace MovieStoreApi.Repositories;

public class CustomerRepository :GenericRepository<Customer>
{
    public CustomerRepository(MovieStoreContext ctx):base(ctx)
    {
    }

    public override Customer? GetById(Guid id)
    {
       return Context.Customers.Include(customer =>customer.PurchasedMovies).ThenInclude(movie => movie.Movie).FirstOrDefault(customer => customer.Id == id);
    }
}