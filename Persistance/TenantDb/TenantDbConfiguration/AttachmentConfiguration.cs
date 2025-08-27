

namespace Persistence.TenantDb.TenantDbConfiguration;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable(nameof(Attachment));
        builder.Property(x => x.AttachmentId)
            .HasConversion(
                         id => id.Id.ToString(),
                         value => new AttachmentId(Guid.Parse(value)))
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