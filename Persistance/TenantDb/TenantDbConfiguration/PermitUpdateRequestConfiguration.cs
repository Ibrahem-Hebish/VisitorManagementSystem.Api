
namespace Persistence.TenantDb.TenantDbConfiguration;

public class PermitUpdateRequestConfiguration : IEntityTypeConfiguration<PermitUpdateRequest>
{
    public void Configure(EntityTypeBuilder<PermitUpdateRequest> builder)
    {
        builder.ToTable(nameof(PermitUpdateRequest));
        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Id.ToString(),
                         value => new PermitUpdateRequestId(Guid.Parse(value)))
            .IsRequired();

        builder.HasKey(x => x.Id);

        builder.Property(pur => pur.CreatedAt)
            .IsRequired();

        builder.Property(pur => pur.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(pur => pur.PermitUpdateAction)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.BranchId)
            .HasConversion(
                         id => id.Guid.ToString(),
                         value => new BranchId(Guid.Parse(value)))
            .IsRequired();

        builder.Property(x => x.PermitId)
           .HasConversion(
                        id => id.Id.ToString(),
                        value => new PermitId(Guid.Parse(value)))
                        .IsRequired();
    }
}
