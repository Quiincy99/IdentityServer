using TestInitProject.Domain.Common;

namespace TestInitProject.Domain.Entities;

public class Customer : BaseAuditableEntity<Guid>
{
    public Customer(Guid id) : base(id)
    {
    }

    public string Email { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;

    public Role Role { get; private set; } = Role.Registered;
    public int RoleId { get; private set; } = Role.Registered.Value;

    public Customer Create(string email, string name, Role? role = null)
    {
        this.Email = email;
        this.Name = name;
        this.Role = role is null ? Role.Registered : role;
        this.RoleId = role is null ? Role.Registered.Value : role.Value;

        return this;
    }
}
