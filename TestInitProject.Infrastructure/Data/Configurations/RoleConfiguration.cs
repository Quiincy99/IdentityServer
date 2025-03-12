using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestInitProject.Domain;
using TestInitProject.Domain.Entities;

namespace TestInitProject.Infrastructure;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Role);

        builder.HasKey(x => x.Value);

        builder.Property(x => x.Name).HasMaxLength(50);

        builder.HasMany(x => x.Users)
            .WithOne(x => x.Role);

        builder.HasMany(x => x.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasData(Role.List);
    }
}
