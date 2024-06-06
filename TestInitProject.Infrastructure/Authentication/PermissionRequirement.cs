using Microsoft.AspNetCore.Authorization;

namespace TestInitProject.Infrastructure;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}
