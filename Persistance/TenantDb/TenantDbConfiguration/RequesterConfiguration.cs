namespace Persistence.TenantDb.TenantDbConfiguration;

public class RequesterConfiguration : IEntityTypeConfiguration<Requester>
{
    public void Configure(EntityTypeBuilder<Requester> builder)
    {
        builder.ToTable(nameof(Requester));

        builder.HasMany(x => x.PermitUpdateRequest)
            .WithOne(x => x.Requester)
            .HasForeignKey(x => x.RequesterId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}