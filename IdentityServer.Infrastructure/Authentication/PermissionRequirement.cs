﻿using Microsoft.AspNetCore.Authorization;

namespace IdentityServer.Infrastructure;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}
