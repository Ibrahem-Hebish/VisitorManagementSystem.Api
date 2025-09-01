namespace Persistence.CentralTenantsDatabase.Configurations;

public class SharedTenantConfiguration : IEntityTypeConfiguration<SharedTenant>
{
    public void Configure(EntityTypeBuilder<SharedTenant> builder)
    {
        builder.ToTable("SharedTenants");

        builder.HasKey(x => x.Id);

        builder.Navigation(t => t.Manager)
            .AutoInclude();

        builder.Property(x => x.Id)
            .HasConversion(
                           id => id.Value,
                           value => new SharedTenantId(value));

        builder.Property(x => x.ManagerId)
            .HasConversion(
                           id => id.Value,
                           value => new SharedUserId(value));

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);


        builder.HasMany(x => x.Branches)
            .WithOne(x => x.Tenant)
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Manager)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);


    }
}
