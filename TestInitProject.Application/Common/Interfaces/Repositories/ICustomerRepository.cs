using TestInitProject.Domain.Customers;

namespace TestInitProject.Application;

public interface ICustomerRepository : IRepository<Customer>
{
    public Task<bool> CheckUniqueEmailAsync(string email);
}
