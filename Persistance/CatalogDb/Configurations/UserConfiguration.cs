namespace Persistence.CentralTenantsDatabase.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<SharedUser>
{
    public void Configure(EntityTypeBuilder<SharedUser> builder)
    {
        builder.ToTable(nameof(SharedUser));

        builder.HasKey(x => x.Id);


        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Value,
                         value => new SharedUserId(value));


        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(x => x.BranchId)
            .HasConversion(
                         id => id.Value,
                         value => new SharedBranchId(value))
            .IsRequired(false);

    }
}
