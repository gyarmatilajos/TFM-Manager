using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Data.Entities.Companies
{
    public class CompanyAccountingContact : IEntityTypeConfiguration<CompanyAccountingContact>
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        public void Configure(EntityTypeBuilder<CompanyAccountingContact> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CompanyId)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Email)
                .HasMaxLength(256);

            builder.Property(x => x.Phone)
                .HasMaxLength(50);

            builder.Property(x => x.Role)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(2000);

            builder.HasOne(x => x.Company)
                .WithMany(x => x.AccountingContacts)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.CompanyId);

        }
    }
}
