using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestInitProject.Domain.Entities;

namespace TestInitProject.Infrastructure;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permission);

        builder.HasKey(x => x.Id);

        IEnumerable<Permission> permissions = Enum
            .GetValues<Domain.Enums.Permissions>()
            .Select(x => new Permission
            {
                Id = (int)x,
                Name = x.ToString()
            });

        builder.HasData(permissions);
    }
}
