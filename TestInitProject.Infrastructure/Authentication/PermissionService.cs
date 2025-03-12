using System.Security;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TestInitProject.Domain.Entities;
using TestInitProject.Infrastructure.Data;

namespace TestInitProject.Infrastructure;

internal class PermissionService : IPermissionService
{
    private readonly ApplicationDbContext _context;
    public PermissionService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<HashSet<string>> GetPermissionAsync(Guid userId)
    {
        var role = await _context.Set<User>()
            .Include(x => x.Role)
            .ThenInclude(x => x.Permissions)
            .Where(x => x.Id == userId)
            .Select(x => x.Role)
            .FirstAsync();

        return role.Permissions is null ?
            new HashSet<string>() :
            role.Permissions.Select(x => x.Name).ToHashSet();
    }
}
