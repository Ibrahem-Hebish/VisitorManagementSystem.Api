

using Domain.TenantDomain.Attachments;
using Domain.TenantDomain.Attachments.ObjectValues;

namespace Persistence.TenantDb.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable(nameof(Attachment));
        builder.Property(x => x.AttachmentId)
            .HasConversion(
                         id => id.Value,
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
                         id => id.Value,
                         value => new BranchId(value))
            .IsRequired();

        builder.Property(x => x.PermitId)
           .HasConversion(
                        id => id.Value,
                        value => new PermitId(value))
                        .IsRequired();
    }
}