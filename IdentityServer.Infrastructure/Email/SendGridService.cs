using System;
using IdentityServer.Application.Common.Interfaces.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace IdentityServer.Infrastructure.Email;

public class SendGridService : IEmailService
{
    private readonly SendGridSettings _sendGridOptions;
    public SendGridService(IOptions<SendGridSettings> options)
    {
        _sendGridOptions = options.Value;
    }
    public async Task SendPasswordResetEmailAsync(string email, string token)
    {
        var client = new SendGridClient(_sendGridOptions.ApiKey);
        var from = new EmailAddress(_sendGridOptions.FromEmail, _sendGridOptions.FromName);
        var to = new EmailAddress(email);
        var subject = "Password Reset Request";
        var resetLink = $"https://yourapp.com/reset?token={token}";
        var htmlContent = $"<p>Click <a href='{resetLink}'>here</a> to reset your password. This link expires in 1 hour.</p>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
        var response = await client.SendEmailAsync(msg);

        Console.WriteLine(await response.Body.ReadAsStringAsync());
    }
}
