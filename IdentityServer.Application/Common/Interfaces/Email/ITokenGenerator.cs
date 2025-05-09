using System;

namespace IdentityServer.Application.Common.Interfaces.Email;

public interface ITokenGenerator
{
    string GenerateToken(int length = 32);
}
