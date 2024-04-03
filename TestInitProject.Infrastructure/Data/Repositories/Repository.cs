using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TestInitProject.Application;
using TestInitProject.Domain.Common;
using TestInitProject.Infrastructure.Data;

namespace TestInitProject.Infrastructure;

internal abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity<Guid> 
{
    protected readonly ApplicationDbContext DbContext;
    public Repository(ApplicationDbContext context)
    {
        DbContext = context;
    }

    public void Add(TEntity entity)
    {
        DbContext.Add(entity);
    }

    public IQueryable<TEntity> AsQueryable() => DbContext.Set<TEntity>().AsQueryable();

    public void Update(TEntity entity)
    {
        DbContext.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        DbContext.Remove(entity);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
    }
}
