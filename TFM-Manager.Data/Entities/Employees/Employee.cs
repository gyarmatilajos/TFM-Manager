using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Data.Entities.Companies;
using TFM_Manager.Data.Entities.Documents;
using TFM_Manager.Data.Entities.Identity;
using TFM_Manager.Data.Entities.Lookups;

namespace TFM_Manager.Data.Entities.Employees
{
    public class Employee : IEntityTypeConfiguration<Employee>
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public int EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; } = null!;

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string TaxIdentificationNumber { get; set; } = string.Empty;
        public string? SocialSecurityNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? MotherName { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        public AppllicationUser? ApplicationUser { get; set; }

        public ICollection<CompanyEmployee> CompanyEmployees { get; set; } = new List<CompanyEmployee>();
        public ICollection<EmployeeDocument> Documents { get; set; } = new List<EmployeeDocument>();
        public ICollection<Company> RepresentedCompanies { get; set; } = new List<Company>();



        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.EmployeeTypeId)
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasMaxLength(256);

            builder.Property(x => x.Address)
                .HasMaxLength(500);

            builder.Property(x => x.TaxIdentificationNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.SocialSecurityNumber)
                .HasMaxLength(20);

            builder.Property(x => x.PlaceOfBirth)
                .HasMaxLength(150);

            builder.Property(x => x.MotherName)
                .HasMaxLength(200);

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(2000);

            builder.HasOne(x => x.EmployeeType)
                .WithMany()
                .HasForeignKey(x => x.EmployeeTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.TaxIdentificationNumber)
                .IsUnique();

            builder.HasIndex(x => x.EmployeeTypeId);

        }
    }
}
