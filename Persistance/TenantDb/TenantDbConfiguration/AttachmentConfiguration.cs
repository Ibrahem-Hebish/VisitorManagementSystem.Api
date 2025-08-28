

namespace Persistence.TenantDb.TenantDbConfiguration;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable(nameof(Attachment));
        builder.Property(x => x.AttachmentId)
            .HasConversion(
                         id => id.Id,
                         value => new AttachmentId(value))
            .IsRequired();

        builder.HasKey(x => x.AttachmentId);

        builder.Property(x => x.Type)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.FilePath)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.BranchId)
            .HasConversion(
                         id => id.Guid,
                         value => new BranchId(value))
            .IsRequired();

        builder.Property(x => x.PermitId)
           .HasConversion(
                        id => id.Id,
                        value => new PermitId(value))
                        .IsRequired();
    }
}