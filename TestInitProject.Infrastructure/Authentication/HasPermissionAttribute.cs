using Microsoft.AspNetCore.Authorization;

namespace TestInitProject.Infrastructure;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        :base(policy: permission)
    {
        
    }
}
