using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TestInitProject.Application;
using TestInitProject.Domain.Common;

namespace TestInitProject.Infrastructure;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly IUser _user;
    private readonly TimeProvider _timeProvider;

    public AuditableEntityInterceptor(
        IUser user,
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
                var userId = Guid.TryParse(_user.Id, out var id) ? id : Guid.Empty;
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
