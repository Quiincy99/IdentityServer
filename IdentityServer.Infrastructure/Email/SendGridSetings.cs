using System;

namespace IdentityServer.Infrastructure.Email;

public class SendGridSettings
{
    public readonly static string SectionName = "SendGrid";
    public string ApiKey { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
}
