
using TestInitProject.Domain.Entities;

namespace TestInitProject.Application;

public interface ICustomerRepository : IRepository<Customer>
{
    public Task<bool> CheckUniqueEmailAsync(string email);

    public Task<Customer?> GetCustomerByEmailAsync(string email);
}
