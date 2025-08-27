namespace Persistence.TenantDb.TenantDbConfiguration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(nameof(Employee));

        builder.Property(x => x.BranchId)
            .HasConversion(
                         id => id.Guid.ToString(),
                         value => new BranchId(Guid.Parse(value)))
            .IsRequired();

        builder.Property(x => x.Position)
            .HasConversion<int>()
            .IsRequired();

        builder.HasMany(e => e.PermitTracks)
             .WithOne(pt => pt.HandledBy)
             .HasForeignKey(pt => pt.EmployeeId)
             .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Manager)
            .WithMany()
            .HasForeignKey(e => e.ManagerId)
            .OnDelete(DeleteBehavior.NoAction);


    }
}