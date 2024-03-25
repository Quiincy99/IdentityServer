using Microsoft.EntityFrameworkCore;
using TestInitProject.Domain;

namespace TestInitProject.Application;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
