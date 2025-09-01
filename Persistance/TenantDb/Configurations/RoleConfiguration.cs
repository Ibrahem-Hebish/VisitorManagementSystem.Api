
using Domain.TenantDomain.Roles;
using Domain.TenantDomain.Roles.ObjectValues;

namespace Persistence.TenantDb.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Role));

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Id)
           .HasConversion(
                        id => id.Value,
                        value => new RoleId(value))
                        .IsRequired();

        builder.HasKey(x => x.Id);

    }
}