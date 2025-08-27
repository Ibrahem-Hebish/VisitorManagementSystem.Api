namespace Persistence.TenantDb.TenantDbConfiguration;

public class VisitorConfiguration : IEntityTypeConfiguration<Visitor>
{
    public void Configure(EntityTypeBuilder<Visitor> builder)
    {
        builder.ToTable(nameof(Visitor));
        builder.Property(x => x.VisitorId)
            .HasConversion(
                         id => id.Id.ToString(),
                         value => new VisitorId(Guid.Parse(value)))
            .IsRequired();

        builder.HasKey(x => x.VisitorId);

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
                         id => id.Guid.ToString(),
                         value => new BranchId(Guid.Parse(value)))
            .IsRequired();

    }
}