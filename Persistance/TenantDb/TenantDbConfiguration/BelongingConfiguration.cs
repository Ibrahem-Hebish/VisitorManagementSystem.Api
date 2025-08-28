
namespace Persistence.TenantDb.TenantDbConfiguration;

public class BelongingConfiguration : IEntityTypeConfiguration<Belonging>
{
    public void Configure(EntityTypeBuilder<Belonging> builder)
    {
        builder.ToTable(nameof(Belonging));
        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Id,
                         value => new BelongingId(value))
            .IsRequired();

        builder.HasKey(x => x.Id);

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