using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using IdentityServer.Application;
using IdentityServer.Domain.Entities;
using IdentityServer.Infrastructure.Authentication;

namespace IdentityServer.Infrastructure;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;
    private readonly IPermissionService _permissionService;
    public JwtProvider(IOptions<JwtSettings> options, IPermissionService permissionService)
    {
        _jwtSettings = options.Value;
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
                Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)
            ),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
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
