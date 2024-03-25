using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestInitProject.Domain;

namespace TestInitProject.Infrastructure;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Email).HasMaxLength(100);

        builder.Property(c => c.Name).HasMaxLength(100);

        builder.HasIndex(x => x.Email).IsUnique();
    }
}
