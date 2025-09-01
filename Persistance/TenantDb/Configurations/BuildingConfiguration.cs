using Domain.TenantDomain.Buildings;
using Domain.TenantDomain.Buildings.ObjectValues;

namespace Persistence.TenantDb.Configurations;

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
                           id => id.Value,
                           value => new BuildingId(value))
            .IsRequired();

        builder.Property(x => x.FloorNumbers)
            .IsRequired();

        builder.Property(x => x.BranchId)
            .HasConversion(
                         id => id.Value,
                         value => new BranchId(value))
            .IsRequired();


    }
}