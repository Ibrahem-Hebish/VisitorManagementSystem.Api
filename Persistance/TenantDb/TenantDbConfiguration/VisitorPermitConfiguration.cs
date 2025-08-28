using Domain.VisitorPermits;

namespace Persistence.TenantDb.TenantDbConfiguration;

public class VisitorPermitConfiguration : IEntityTypeConfiguration<VisitorPermit>
{
    public void Configure(EntityTypeBuilder<VisitorPermit> builder)
    {
        builder.ToTable(nameof(VisitorPermit));

        builder.Property(x => x.PermitId)
           .HasConversion(
            Id => Id.Id,
            value => new PermitId(value))
           .IsRequired();

        builder.Property(x => x.VisitorId)
            .HasConversion(
             Id => Id.Id,
             value => new VisitorId(value))
            .IsRequired();

        builder.Property(x => x.BranchId)
            .HasConversion(
             Id => Id.Guid,
             value => new BranchId(value))
            .IsRequired();

        builder.HasKey(x => new { x.PermitId, x.VisitorId });


    }
}
