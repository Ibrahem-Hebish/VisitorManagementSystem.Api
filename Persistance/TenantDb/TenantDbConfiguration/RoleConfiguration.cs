
namespace Persistence.TenantDb.TenantDbConfiguration;

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
                        id => id.Id,
                        value => new RoleId(value))
                        .IsRequired();

        builder.HasKey(x => x.Id);

    }
}