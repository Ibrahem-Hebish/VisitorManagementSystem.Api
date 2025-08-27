namespace Persistence.CentralTenantsDatabase.Configurations;

public class SharedBranchConfiguration : IEntityTypeConfiguration<SharedBranch>
{
    public void Configure(EntityTypeBuilder<SharedBranch> builder)
    {
        builder.ToTable(nameof(SharedBranch));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Guid.ToString(),
                         id => new SharedBranchId(Guid.Parse(id)));


        builder.OwnsOne(x => x.Address);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.Users)
            .WithOne(x => x.Branch)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.TenantId)
            .HasConversion(builder => builder.Id.ToString(),
                           value => new SharedTenantId(Guid.Parse(value)));

        builder.HasMany(x => x.sharedUserTokens)
           .WithOne(x => x.SharedBranch)
           .HasForeignKey(x => x.BranchId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
