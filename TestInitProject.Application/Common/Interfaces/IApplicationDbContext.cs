using Microsoft.EntityFrameworkCore;
using TestInitProject.Domain.Customers;

namespace TestInitProject.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
