using Domain.TenantDomain.Belongings;

namespace Persistence.TenantDb.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable(nameof(Car));

        builder.Property(x => x.Color)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.PlateNumber)
            .HasMaxLength(20)
            .IsRequired();
    }
}