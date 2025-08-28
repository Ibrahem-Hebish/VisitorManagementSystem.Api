namespace Persistence.TenantDb.TenantDbConfiguration;

public class VisitorConfiguration : IEntityTypeConfiguration<Visitor>
{
    public void Configure(EntityTypeBuilder<Visitor> builder)
    {
        builder.ToTable(nameof(Visitor));
        builder.Property(x => x.VisitorId)
            .HasConversion(
                         id => id.Id,
                         value => new VisitorId(value))
            .IsRequired();

        builder.HasKey(x => x.VisitorId);

        builder.HasIndex(x => x.Email)
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
                         id => id.Guid,
                         value => new BranchId(value))
            .IsRequired();

    }
}