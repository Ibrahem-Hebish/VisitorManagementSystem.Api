namespace Persistence.TenantDb.TenantDbConfiguration;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(nameof(UserToken));
        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Id,
                         value => new UserTokenId(value))
            .IsRequired();

        builder.HasKey(x => x.Id);
    }
}