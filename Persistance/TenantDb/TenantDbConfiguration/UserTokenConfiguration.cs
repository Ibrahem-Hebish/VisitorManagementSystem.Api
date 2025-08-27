namespace Persistence.TenantDb.TenantDbConfiguration;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(nameof(UserToken));
        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Id.ToString(),
                         value => new UserTokenId(Guid.Parse(value)))
            .IsRequired();

        builder.HasKey(x => x.Id);
    }
}