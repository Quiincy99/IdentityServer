using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using TestInitProject.Application;
using TestInitProject.Domain.Entities;
using TestInitProject.Infrastructure.Data;

namespace TestInitProject.Infrastructure;

internal class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> CheckUniqueEmailAsync(string email)
    {
        return !await DbContext.Set<Customer>().AnyAsync(x => x.Email.ToLower() == email.ToLower());
    }

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        return await DbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
    }
}
