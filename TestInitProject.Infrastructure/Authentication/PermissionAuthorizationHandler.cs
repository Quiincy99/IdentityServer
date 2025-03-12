using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using TestInitProject.Infrastructure.Authentication;

namespace TestInitProject.Infrastructure;

public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        // string? userId = context.User.Claims.FirstOrDefault(x => x.Type is ClaimTypes.NameIdentifier)?.Value;
    
        // if (!Guid.TryParse(userId, out Guid parsedId))
        // {
        //     return;
        // } 

        // using IServiceScope scope = _serviceScopeFactory.CreateScope();

        // IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        // HashSet<string> permissions = await permissionService
        //     .GetPermissionAsync(parsedId);

        HashSet<string> permissions = context.User.Claims
            .Where(x => x.Type == CustomClaims.Permissions)
            .Select(x => x.Value)
            .ToHashSet();

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
