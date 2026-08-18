using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Data.Entities.Companies
{
    public class CompanyBankAccount : IEntityTypeConfiguration<CompanyBankAccount>
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        public string? BankName { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string? IBAN { get; set; }
        public string? SWIFTBIC { get; set; }
        public string Currency { get; set; } = "HUF";
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        public void Configure(EntityTypeBuilder<CompanyBankAccount> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CompanyId)
                .IsRequired();

            builder.Property(x => x.BankName)
                .HasMaxLength(150);

            builder.Property(x => x.AccountNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.IBAN)
                .HasMaxLength(50);

            builder.Property(x => x.SWIFTBIC)
                .HasMaxLength(20);

            builder.Property(x => x.Currency)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(x => x.IsDefault)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(2000);

            builder.HasOne(x => x.Company)
                .WithMany(x => x.BankAccounts)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.CompanyId);

        }
    }
}
