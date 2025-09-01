namespace Persistence.TenantDb.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(nameof(Employee));

        builder.Property(x => x.BranchId)
            .HasConversion(
                         id => id.Value,
                         value => new BranchId(value))
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