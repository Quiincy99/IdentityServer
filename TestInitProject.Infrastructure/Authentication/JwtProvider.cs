using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TestInitProject.Application;
using TestInitProject.Domain.Entities;
using TestInitProject.Infrastructure.Authentication;

namespace TestInitProject.Infrastructure;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly IPermissionService _permissionService;
    public JwtProvider(IOptions<JwtOptions> options, IPermissionService permissionService)
    {
        _jwtOptions = options.Value;
        _permissionService = permissionService;
    }
    public async Task<string> GenerateAsync(User user)
    {
        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var permissions = await _permissionService
            .GetPermissionAsync(user.Id);

        foreach (var permission in permissions)
        {
            claims.Add(new Claim(CustomClaims.Permissions, permission));
        }

        var signingCredentals = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)
            ),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentals
        );

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
