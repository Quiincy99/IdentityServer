namespace IdentityServer.Infrastructure;

public class JwtSettings
{
    public static readonly string SectionName = "Jwt";
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
}
