namespace Persistence.TenantDb.TenantDbConfiguration;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable(nameof(Tenant));
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();


        builder.Property(x => x.Id)
           .HasConversion(
                     id => id.Guid,
                     value => new TenantId(value))
                     .IsRequired();

        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Branches)
            .WithOne(x => x.Tenant)
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.NoAction);



    }
}
