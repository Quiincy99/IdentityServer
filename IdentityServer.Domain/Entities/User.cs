﻿using BCrypt.Net;
using IdentityServer.Domain.Common;

namespace IdentityServer.Domain.Entities;

public class User : BaseAuditableEntity<Guid>
{
    public User(Guid id) : base(id)
    {
    }

    public string Email { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string HashedPassword { get; private set; } = string.Empty;
    public Role Role { get; private set; } = null!;
    public int RoleId { get; private set; } = Role.Registered.Value;
    public IList<PasswordResetToken>? PasswordResetTokens { get; private set; }

    public User Create(string email, string name, string password)
    {
        this.Email = email;
        this.Name = name;
        this.HashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        this.RoleId = Role.Registered.Value;

        return this;
    }

    public User UpdatePassword(string newPasswordHash)
    {
        if (string.IsNullOrEmpty(newPasswordHash))
            throw new ArgumentException("Password hash cannot be empty");
        this.HashedPassword = newPasswordHash;

        return this;
    }

    public User SetRole(Role role)
    {
        this.Role = role;

        return this;
    }
}
