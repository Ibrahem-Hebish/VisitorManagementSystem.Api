namespace Persistence.TenantDb.TenantDbConfiguration;

public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.ToTable(nameof(Manager));


    }
}