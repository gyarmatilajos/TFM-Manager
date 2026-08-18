using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Data.Entities.Documents;
using TFM_Manager.Data.Entities.Employees;
using TFM_Manager.Data.Entities.Partners;

namespace TFM_Manager.Data.Entities.Companies
{
    public class Company : IEntityTypeConfiguration<Company>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
        public string? CompanyRegistrationNumber { get; set; }
        public string RegisteredAddress { get; set; } = string.Empty;
        public string? MailingAddress { get; set; }
        public string? BillingEmail { get; set; }

        public int? RepresentativeEmployeeId { get; set; }
        public Employee? RepresentativeEmployee { get; set; }

        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        public ICollection<CompanyBankAccount> BankAccounts { get; set; } = new List<CompanyBankAccount>();
        public ICollection<CompanyAccountingContact> AccountingContacts { get; set; } = new List<CompanyAccountingContact>();
        public ICollection<CompanyEmployee> CompanyEmployees { get; set; } = new List<CompanyEmployee>();
        public ICollection<PartnerCompany> PartnerCompanies { get; set; } = new List<PartnerCompany>();
        public ICollection<CompanyDocument> Documents { get; set; } = new List<CompanyDocument>();

        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.TaxNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.CompanyRegistrationNumber)
                .HasMaxLength(50);

            builder.Property(x => x.RegisteredAddress)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.MailingAddress)
                .HasMaxLength(500);

            builder.Property(x => x.BillingEmail)
                .HasMaxLength(256);

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(2000);

            builder.HasOne(x => x.RepresentativeEmployee)
                .WithMany(x => x.RepresentedCompanies)
                .HasForeignKey(x => x.RepresentativeEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.TaxNumber)
                .IsUnique();

        }
    }
}
