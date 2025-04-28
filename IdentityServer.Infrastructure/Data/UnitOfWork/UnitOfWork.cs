using IdentityServer.Application;
using IdentityServer.Infrastructure.Data;

namespace IdentityServer.Infrastructure;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _applicationDbContext;
    public UnitOfWork(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}
