namespace Persistence.CentralTenantsDatabase.Configurations;

public class SharedTenantConfiguration : IEntityTypeConfiguration<SharedTenant>
{
    public void Configure(EntityTypeBuilder<SharedTenant> builder)
    {
        builder.ToTable("SharedTenants");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                           id => id.Id.ToString(),
                           id => new SharedTenantId(Guid.Parse(id)));

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);


        builder.HasMany(x => x.Branches)
            .WithOne(x => x.Tenant)
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Cascade);



    }
}
