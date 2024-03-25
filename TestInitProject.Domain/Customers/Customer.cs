using TestInitProject.Domain.Common;

namespace TestInitProject.Domain;

public class Customer : BaseAuditableEntity<Guid>
{
    public Customer(Guid id) : base(id)
    {
    }

    public string Email { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;

    public Customer Create(string email, string name)
    {
        this.Email = email;
        this.Name = name;

        return this;
    }
}
