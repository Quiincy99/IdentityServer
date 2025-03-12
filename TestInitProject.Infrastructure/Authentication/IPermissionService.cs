namespace TestInitProject.Infrastructure;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionAsync(Guid userId);
}
