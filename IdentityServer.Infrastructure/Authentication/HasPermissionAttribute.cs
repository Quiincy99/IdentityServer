using Microsoft.AspNetCore.Authorization;
using IdentityServer.Domain.Enums;

namespace IdentityServer.Infrastructure.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permissions permission)
        : base(policy: permission.ToString())
    {

    }
}
