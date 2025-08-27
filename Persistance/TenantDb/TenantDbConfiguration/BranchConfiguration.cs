namespace Persistence.TenantDb.TenantDbConfiguration;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable(nameof(Branch));

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Id)
            .HasConversion(
                         id => id.Guid.ToString(),
                         value => new BranchId(Guid.Parse(value)))
                         .IsRequired();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();

        builder.OwnsOne(x => x.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.Street)
                .HasMaxLength(200)
                .IsRequired();

            addressBuilder.Property(a => a.City)
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.Country)
                .HasMaxLength(100)
                .IsRequired();

        });

        builder.Property(x => x.TenantId)
            .HasConversion(
                          id => id.Guid.ToString(),
                          value => new TenantId(Guid.Parse(value)))
            .IsRequired();


        builder.HasMany(b => b.Buildings)
            .WithOne(b => b.Branch)
            .HasForeignKey(b => b.BranchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.Employees)
            .WithOne(u => u.Branch)
            .HasForeignKey(u => u.BranchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.Belongings)
            .WithOne(b => b.Branch)
            .HasForeignKey(b => b.BranchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.EntryLogs)
            .WithOne(el => el.Branch)
            .HasForeignKey(el => el.BranchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.Permits)
            .WithOne(p => p.Branch)
            .HasForeignKey(p => p.BranchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.Attachments)
            .WithOne(a => a.Branch)
            .HasForeignKey(a => a.BranchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.Visitors)
            .WithOne(v => v.Branch)
            .HasForeignKey(v => v.BranchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.PermitTrack)
            .WithOne(pt => pt.Branch)
            .HasForeignKey(pt => pt.BranchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.PermitUpdateRequest)
            .WithOne(pur => pur.Branch)
            .HasForeignKey(pur => pur.BranchId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}
