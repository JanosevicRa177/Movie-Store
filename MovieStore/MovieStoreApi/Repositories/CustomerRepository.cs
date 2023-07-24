using MovieStore.Core.Model;
using MovieStore.Infrastructure;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Repositories;

public class CustomerRepository :GenericRepository<Customer>
{
    public CustomerRepository(MovieStoreContext ctx):base(ctx)
    {
    }
}