using MovieStore.Core.Model;

namespace MovieStoreApi.Repositories.Interfaces;

public interface ICustomerRepository: IRepository<Customer>
{
    Customer? GetByEmail(string email);
}