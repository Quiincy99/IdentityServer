using System;
using IdentityServer.Domain.Common;

namespace IdentityServer.Domain.Entities;

public class PasswordResetToken : BaseEntity<Guid>
{
    public PasswordResetToken(Guid id) : base(id)
    {
    }

    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }
    public User User { get; set; }

    public bool IsValid()
    {
        return !IsUsed && ExpiryDate > DateTime.UtcNow;
    }
}
