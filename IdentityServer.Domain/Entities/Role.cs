using Ardalis.SmartEnum;

namespace IdentityServer.Domain.Entities;

public sealed class Role : SmartEnum<Role>
{
    public static readonly Role Registered = new(nameof(Registered), 1);
    public static readonly Role Admin = new Role(nameof(Admin), 2);

    public Role(string name, int value) : base(name, value)
    {
    }

    public ICollection<Permission>? Permissions { get; set; }
    public ICollection<User>? Users { get; set; }
}
