using EnterpriseHRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseHRMS.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EmployeeCode)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasIndex(x => x.EmployeeCode)
               .IsUnique();

        builder.Property(x => x.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.LastName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.HasIndex(x => x.Email)
               .IsUnique();

        builder.Property(x => x.PhoneNumber)
               .HasMaxLength(15);

        builder.Property(x => x.Salary)
               .HasPrecision(18, 2);

        builder.Property(x => x.IsActive)
               .HasDefaultValue(true);

        builder.HasOne(x => x.Department)
               .WithMany(x => x.Employees)
               .HasForeignKey(x => x.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}