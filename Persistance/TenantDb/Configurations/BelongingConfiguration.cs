
using Domain.TenantDomain.Belongings;
using Domain.TenantDomain.Belongings.ObjectValues;

namespace Persistence.TenantDb.Configurations;

public class BelongingConfiguration : IEntityTypeConfiguration<Belonging>
{
    public void Configure(EntityTypeBuilder<Belonging> builder)
    {
        builder.ToTable(nameof(Belonging));
        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Value,
                         value => new BelongingId(value))
            .IsRequired();

        builder.HasKey(x => x.Id);

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