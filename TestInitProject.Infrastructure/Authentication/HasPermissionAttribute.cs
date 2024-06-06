using Microsoft.AspNetCore.Authorization;
using TestInitProject.Domain.Enums;

namespace TestInitProject.Infrastructure.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permissions permission)
        :base(policy: permission.ToString())
    {
        
    }
}
