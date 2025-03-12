using TestInitProject.Domain.Common;

namespace TestInitProject.Domain.Entities;

public class User : BaseAuditableEntity<Guid>
{
    public User(Guid id) : base(id)
    {
    }

    public string Email { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public Role Role { get; private set; } = null!;
    public int RoleId { get; private set; } = Role.Registered.Value;

    public User Create(string email, string name)
    {
        this.Email = email;
        this.Name = name;

        this.RoleId = Role.Registered.Value;

        return this;
    }
}
