using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using IdentityServer.Application.Common.Interfaces.Auth;
using IdentityServer.Domain.Common;

namespace IdentityServer.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly IUserContext _user;
    private readonly TimeProvider _timeProvider;

    public AuditableEntityInterceptor(
        IUserContext user,
        TimeProvider timeProvider)
    {
        _user = user;
        _timeProvider = timeProvider;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity<Guid>>())
        {
            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                var utcNow = _timeProvider.GetUtcNow();
                Guid userId = _user.Id ?? Guid.Empty;
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.Created = utcNow;
                }

                entry.Entity.LastModifiedBy = userId;
                entry.Entity.LastModified = utcNow;
            }
        }
    }
}
