namespace Persistence.CentralTenantsDatabase.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<SharedUser>
{
    public void Configure(EntityTypeBuilder<SharedUser> builder)
    {
        builder.ToTable(nameof(SharedUser));

        builder.HasKey(x => x.Id);


        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Id.ToString(),
                         id => new SharedUserId(Guid.Parse(id)));


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
                         id => id == null ? null : id.Guid.ToString(),
                         id => id == null ? null : new SharedBranchId(Guid.Parse(id)))
            .IsRequired(false);

    }
}
