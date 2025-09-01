using Domain.TenantDomain.Tokens;
using Domain.TenantDomain.Tokens.ValueObjects;

namespace Persistence.TenantDb.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(nameof(UserToken));
        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Value,
                         value => new UserTokenId(value))
            .IsRequired();

        builder.HasKey(x => x.Id);
    }
}