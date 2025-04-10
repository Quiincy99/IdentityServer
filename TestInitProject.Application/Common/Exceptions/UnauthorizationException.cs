using System;

namespace TestInitProject.Application.Common.Exceptions;

public class UnauthorizationException : Exception
{
    public UnauthorizationException() : base("Unauthorized access")
    {
    }
}

