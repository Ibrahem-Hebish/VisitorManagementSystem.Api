namespace Persistence.CentralTenantsDatabase.Configurations;

public class SharedUserTokenConfiguration : IEntityTypeConfiguration<SharedUserToken>
{
    public void Configure(EntityTypeBuilder<SharedUserToken> builder)
    {
        builder.ToTable(nameof(SharedUserToken));

        builder.HasKey(x => x.Id);


        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Value.ToString(),
                         id => new SharedUserTokenId(Guid.Parse(id)));

        builder.Property(x => x.AccessToken)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.AccessTokenExpiredDate)
            .IsRequired();

        builder.Property(x => x.RefreshToken)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.RefreshTokenExpiredDate)
            .IsRequired();

        builder.Property(x => x.InUse)
            .IsRequired();

        builder.Property(x => x.BranchId)
            .HasConversion(
                          id => id == null ? null : id.Guid.ToString(),
                          id => id == null ? null : new SharedBranchId(Guid.Parse(id)))
            .IsRequired(false);
    }
}
