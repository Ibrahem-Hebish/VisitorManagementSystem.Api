
namespace Persistence.TenantDb.TenantDbConfiguration;

public class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.ToTable(nameof(Building));
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                           id => id.ToString(),
                           value => new BuildingId(Guid.Parse(value)))
            .IsRequired();

        builder.Property(x => x.FloorNumbers)
            .IsRequired();

        builder.Property(x => x.BranchId)
            .HasConversion(
                         id => id.Guid.ToString(),
                         value => new BranchId(Guid.Parse(value)))
            .IsRequired();


    }
}