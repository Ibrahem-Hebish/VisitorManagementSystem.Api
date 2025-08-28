

namespace Persistence.TenantDb.TenantDbConfiguration;

public class EntryLogConfiguration : IEntityTypeConfiguration<Entrylog>
{
    public void Configure(EntityTypeBuilder<Entrylog> builder)
    {
        builder.ToTable(nameof(Entrylog));
        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Id,
                         value => new EntrylogId(value))
            .IsRequired();

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.PermitId, x.BranchId });

        builder.Property(x => x.EntryTime)
            .IsRequired();

        builder.Navigation(x => x.Employee)
            .AutoInclude();

        builder.Navigation(x => x.Visitor)
            .AutoInclude();

        builder.Property(x => x.IsInside)
            .IsRequired();

        builder.Property(x => x.BranchId)
            .HasConversion(
                         id => id.Guid,
                         value => new BranchId(value))
            .IsRequired();

        builder.Property(x => x.VisitorId)
           .HasConversion(
                        id => id.Id,
                        value => new VisitorId(value));

        builder.Property(x => x.AllowedBy)
           .HasConversion(
                        id => id.Id,
                        value => new UserId(value));

        builder.HasOne(x => x.Visitor)
            .WithMany()
            .HasForeignKey(x => x.VisitorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Permit)
                .WithOne()
                .HasForeignKey<Entrylog>(x => x.PermitId)
                .OnDelete(DeleteBehavior.NoAction);
    }
}