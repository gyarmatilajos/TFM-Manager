using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Data.Entities.Lookups;

namespace TFM_Manager.Data.Entities.Employees
{
    public class CompanyEmployeeSalary : IEntityTypeConfiguration<CompanyEmployeeSalary>
    {
        public int Id { get; set; }

        public int CompanyEmployeeId { get; set; }
        public CompanyEmployee CompanyEmployee { get; set; } = null!;

        public int WorkTimeTypeId { get; set; }
        public WorkTimeType WorkTimeType { get; set; } = null!;

        public DateOnly ValidFrom { get; set; }
        public DateOnly? ValidTo { get; set; }
        public decimal NetSalary { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal EmployerContribution { get; set; }
        public decimal EmployeeContribution { get; set; }
        public decimal TotalCompanyCost { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        public void Configure(EntityTypeBuilder<CompanyEmployeeSalary> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CompanyEmployeeId)
                .IsRequired();

            builder.Property(x => x.WorkTimeTypeId)
                .IsRequired();

            builder.Property(x => x.ValidFrom)
                .IsRequired();

            builder.Property(x => x.NetSalary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.GrossSalary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.EmployerContribution)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.EmployeeContribution)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.TotalCompanyCost)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(2000);

            builder.HasOne(x => x.CompanyEmployee)
                .WithMany(x => x.Salaries)
                .HasForeignKey(x => x.CompanyEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.WorkTimeType)
                .WithMany()
                .HasForeignKey(x => x.WorkTimeTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.CompanyEmployeeId);
            builder.HasIndex(x => x.WorkTimeTypeId);

        }
    }
}
