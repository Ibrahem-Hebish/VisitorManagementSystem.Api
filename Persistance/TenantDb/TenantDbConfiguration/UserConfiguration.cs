namespace Persistence.TenantDb.TenantDbConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Id.ToString(),
                         value => new UserId(Guid.Parse(value)))
            .IsRequired();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.HashedPassword)
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(x => x.Gender)
            .HasConversion<int>()
            .IsRequired();

        builder.HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.UserTokens)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}