using Domain.TenantDomain.Visitors;
using Domain.TenantDomain.Visitors.ObjectValues;

namespace Persistence.TenantDb.Configurations;

public class VisitorConfiguration : IEntityTypeConfiguration<Visitor>
{
    public void Configure(EntityTypeBuilder<Visitor> builder)
    {
        builder.ToTable(nameof(Visitor));
        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Value,
                         value => new VisitorId(value))
            .IsRequired();

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.Email, x.BranchId })
            .IsUnique();

        builder.HasIndex(x => x.NationalId)
            .IsUnique();

        builder.Property(v => v.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(v => v.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(v => v.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(v => v.NationalId)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(v => v.Gender)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.BranchId)
            .HasConversion(
                         id => id.Value,
                         value => new BranchId(value))
            .IsRequired();

    }
}