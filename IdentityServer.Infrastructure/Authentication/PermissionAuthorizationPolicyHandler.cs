using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace IdentityServer.Infrastructure;

public class PermissionAuthorizationPolicyHandler
    : DefaultAuthorizationPolicyProvider
{
    private readonly IOptions<AuthorizationOptions> _options;
    public PermissionAuthorizationPolicyHandler(IOptions<AuthorizationOptions> options) : base(options)
    {
        _options = options;
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(
        string policyName)
    {
        AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

        if (policy is null)
        {
            policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(policyName))
                .Build();

            _options.Value.AddPolicy(policyName, policy);
        }

        return policy;
    }
}
