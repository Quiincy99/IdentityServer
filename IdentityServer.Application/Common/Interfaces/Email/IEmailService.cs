using System;

namespace IdentityServer.Application.Common.Interfaces.Email;

public interface IEmailService
{
    Task SendPasswordResetEmailAsync(string email, string token);
}
