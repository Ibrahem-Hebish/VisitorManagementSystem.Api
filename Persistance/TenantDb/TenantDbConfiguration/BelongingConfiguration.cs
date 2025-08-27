
namespace Persistence.TenantDb.TenantDbConfiguration;

public class BelongingConfiguration : IEntityTypeConfiguration<Belonging>
{
    public void Configure(EntityTypeBuilder<Belonging> builder)
    {
        builder.ToTable(nameof(Belonging));
        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Id.ToString(),
                         value => new BelongingId(Guid.Parse(value)))
            .IsRequired();

        builder.HasKey(x => x.Id);

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

        builder.HasOne(x => x.Visitor)
            .WithMany(x => x.Belongings)
            .HasForeignKey(x => x.VisitorId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}