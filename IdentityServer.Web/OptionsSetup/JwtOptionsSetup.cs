using Microsoft.Extensions.Options;
using IdentityServer.Infrastructure;

namespace IdentityServer.Web;

public class JwtOptionsSetup : IConfigureOptions<JwtSettings>
{
    private readonly string SectionName = "Jwt";
    private readonly IConfiguration _configuration;
    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Configure(JwtSettings options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
