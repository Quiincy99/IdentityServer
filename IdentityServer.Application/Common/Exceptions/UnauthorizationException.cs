using System;

namespace IdentityServer.Application.Common.Exceptions;

public class UnauthorizationException : Exception
{
    public UnauthorizationException() : base("Unauthorized access")
    {
    }
}

