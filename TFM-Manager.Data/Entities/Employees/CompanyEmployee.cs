using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Data.Entities.Companies;
using TFM_Manager.Data.Entities.Lookups;

namespace TFM_Manager.Data.Entities.Employees
{
    public class CompanyEmployee : IEntityTypeConfiguration<CompanyEmployee>
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public int EmploymentTypeId { get; set; }
        public EmploymentType EmploymentType { get; set; } = null!;

        public string Position { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        public ICollection<CompanyEmployeeSalary> Salaries { get; set; } = new List<CompanyEmployeeSalary>();

        public void Configure(EntityTypeBuilder<CompanyEmployee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CompanyId)
                .IsRequired();

            builder.Property(x => x.EmployeeId)
                .IsRequired();

            builder.Property(x => x.EmploymentTypeId)
                .IsRequired();

            builder.Property(x => x.Position)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.StartDate)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(2000);

            builder.HasOne(x => x.Company)
                .WithMany(x => x.CompanyEmployees)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Employee)
                .WithMany(x => x.CompanyEmployees)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.EmploymentType)
                .WithMany()
                .HasForeignKey(x => x.EmploymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasIndex(x => x.EmploymentTypeId);
            builder.HasIndex(x => new { x.CompanyId, x.EmployeeId })
                .IsUnique()
                .HasFilter("[IsActive] = 1");

        }
    }
}
