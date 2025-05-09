using System;
using System.Security.Cryptography;
using IdentityServer.Application.Common.Interfaces.Email;

namespace IdentityServer.Infrastructure.Email;

internal sealed class EmailTokenGenerator : ITokenGenerator
{
    public string GenerateToken(int length = 32)
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(length);
        var token = Convert.ToBase64String(tokenBytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");

        return token;
    }
}
