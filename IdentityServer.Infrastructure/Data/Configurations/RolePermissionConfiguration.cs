using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IdentityServer.Domain;
using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Enums;

namespace IdentityServer.Infrastructure.Data.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable(TableNames.RolePermission);

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(
            Create(Role.Registered, Permissions.ReadUser),
            Create(Role.Registered, Permissions.CreateUser)
        );
    }

    public static RolePermission Create(Role role, Permissions permission)
        => new RolePermission
        {
            RoleId = role.Value,
            PermissionId = (int)permission
        };
}
