namespace TestInitProject.Application;

public interface IRepository<TEntity>
{
    public void Add(TEntity entity);

    public void Update(TEntity entity);

    public void Remove(TEntity entity);

    public IQueryable<TEntity> AsQueryable();

    public Task<TEntity?> GetByIdAsync(Guid id);
}
