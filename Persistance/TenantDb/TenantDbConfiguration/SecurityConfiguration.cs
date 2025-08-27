namespace Persistence.TenantDb.TenantDbConfiguration;

public class SecurityConfiguration : IEntityTypeConfiguration<Security>
{
    public void Configure(EntityTypeBuilder<Security> builder)
    {
        builder.ToTable(nameof(Security));

        builder.HasMany(x => x.Entrylogs)
            .WithOne(x => x.Employee)
            .HasForeignKey(x => x.AllowedBy)
            .OnDelete(DeleteBehavior.NoAction);

    }
}