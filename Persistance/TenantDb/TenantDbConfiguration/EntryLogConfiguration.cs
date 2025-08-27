

namespace Persistence.TenantDb.TenantDbConfiguration;

public class EntryLogConfiguration : IEntityTypeConfiguration<Entrylog>
{
    public void Configure(EntityTypeBuilder<Entrylog> builder)
    {
        builder.ToTable(nameof(Entrylog));
        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Id.ToString(),
                         value => new EntrylogId(Guid.Parse(value)))
            .IsRequired();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EntryTime)
            .IsRequired();

        builder.Property(x => x.IsInside)
            .IsRequired();

        builder.Property(x => x.BranchId)
            .HasConversion(
                         id => id.Guid.ToString(),
                         value => new BranchId(Guid.Parse(value)))
            .IsRequired();

        builder.Property(x => x.VisitorId)
           .HasConversion(
                        id => id.Id.ToString(),
                        value => new VisitorId(Guid.Parse(value)));

        builder.Property(x => x.AllowedBy)
           .HasConversion(
                        id => id.Id.ToString(),
                        value => new UserId(Guid.Parse(value)));

        builder.HasOne(x => x.Visitor)
            .WithMany()
            .HasForeignKey(x => x.VisitorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Permit)
                .WithOne()
                .HasForeignKey<Entrylog>(x => x.PermitId);
    }
}