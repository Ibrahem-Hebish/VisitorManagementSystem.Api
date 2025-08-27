namespace Persistence.TenantDb.TenantDbConfiguration;

public class PermitTrackConfiguration : IEntityTypeConfiguration<PermitTrack>
{
    public void Configure(EntityTypeBuilder<PermitTrack> builder)
    {
        builder.ToTable(nameof(PermitTrack));
        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Id.ToString(),
                         value => new PermitTrackId(Guid.Parse(value)))
            .IsRequired();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PermitTrackAction)
            .HasConversion(pta => pta.ToString(),
                           pta => (PermitTrackAction)Enum.Parse(typeof(PermitTrackAction), pta))
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.Description)
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
            value => new PermitId(Guid.Parse(value))
            )
           .IsRequired();


    }
}