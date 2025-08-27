namespace Persistence.TenantDb.TenantDbConfiguration;

public class PermitConfiguration : IEntityTypeConfiguration<Permit>
{
    public void Configure(EntityTypeBuilder<Permit> builder)
    {
        builder.ToTable(nameof(Permit));

        builder.Property(x => x.PermitId)
            .HasConversion(
                         id => id.Id.ToString(),
                         value => new PermitId(Guid.Parse(value)))
                         .IsRequired();


        builder.HasKey(x => x.PermitId);

        builder.Navigation(p => p.Visitor)
                .AutoInclude();

        builder.Navigation(p => p.Building)
               .AutoInclude();

        builder.Property(p => p.Reason)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.HasIndex(p => p.Status);


        builder.Property(p => p.StartDate)
            .IsRequired();

        builder.Property(p => p.EndDate)
            .IsRequired();

        builder.Property(x => x.BranchId)
            .HasConversion(
                         id => id.Guid.ToString(),
                         value => new BranchId(Guid.Parse(value)))
            .IsRequired();

        builder.HasOne(p => p.Building)
             .WithMany(b => b.Permits)
            .HasForeignKey(p => p.BuildingId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(b => b.Requester)
            .WithMany(e => e.Permits)
            .HasForeignKey(p => p.RequestedBy)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(b => b.Handler)
            .WithMany(e => e.Permits)
            .HasForeignKey(p => p.HandledBy)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.Visitor)
            .WithMany(v => v.Permits)
            .HasForeignKey(p => p.VisitorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.Attachments)
            .WithOne(a => a.Permit)
            .HasForeignKey(a => a.PermitId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.PermitTracks)
            .WithOne(pt => pt.Permit)
            .HasForeignKey(pt => pt.PermitId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.PermitUpdateRequest)
            .WithOne(pur => pur.Permit)
            .HasForeignKey(pur => pur.PermitId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.Belongings)
            .WithOne(b => b.Permit)
            .HasForeignKey(b => b.PermitId)
            .OnDelete(DeleteBehavior.NoAction);


    }
}