

using Domain.TenantDomain.EntryLogs;
using Domain.TenantDomain.EntryLogs.ObjectValues;

namespace Persistence.TenantDb.Configurations;

public class EntryLogConfiguration : IEntityTypeConfiguration<Entrylog>
{
    public void Configure(EntityTypeBuilder<Entrylog> builder)
    {
        builder.ToTable(nameof(Entrylog));
        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Value,
                         value => new EntrylogId(value))
            .IsRequired();

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.PermitId, x.BranchId });

        builder.Property(x => x.EntryTime)
            .IsRequired();

        builder.Navigation(x => x.Employee)
            .AutoInclude();

        builder.Property(x => x.IsInside)
            .IsRequired();

        builder.Property(x => x.BranchId)
            .HasConversion(
                         id => id.Value,
                         value => new BranchId(value))
            .IsRequired();

        builder.Property(x => x.PermitId)
           .HasConversion(
                        id => id.Value,
                        value => new PermitId(value));

        builder.Property(x => x.AllowedBy)
           .HasConversion(
                        id => id.Value,
                        value => new UserId(value));

        builder.HasOne(x => x.Permit)
                .WithOne()
                .HasForeignKey<Entrylog>(x => x.PermitId)
                .OnDelete(DeleteBehavior.NoAction);
    }
}