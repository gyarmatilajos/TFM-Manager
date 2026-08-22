using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Data.Entities.Documents;
using TFM_Manager.Data.Entities.Lookups;

namespace TFM_Manager.Data.Entities.Partners
{
    public class Partner : IEntityTypeConfiguration<Partner>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int PartnerTypeId { get; set; }
        public PartnerType PartnerType { get; set; } = null!;

        public string? TaxNumber { get; set; }
        public string? Address { get; set; }
        public string? MailingAddress { get; set; }
        public string? BillingName { get; set; }
        public string? BillingAddress { get; set; }
        public string? BillingEmail { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        public ICollection<PartnerLocation> Locations { get; set; } = new List<PartnerLocation>();
        public ICollection<PartnerContact> Contacts { get; set; } = new List<PartnerContact>();
        public ICollection<PartnerCompany> PartnerCompanies { get; set; } = new List<PartnerCompany>();
        public ICollection<PartnerDocument> Documents { get; set; } = new List<PartnerDocument>();

        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.PartnerTypeId)
                .IsRequired();

            builder.Property(x => x.TaxNumber)
                .HasMaxLength(30);

            builder.Property(x => x.Address)
                .HasMaxLength(500);

            builder.Property(x => x.MailingAddress)
                .HasMaxLength(500);

            builder.Property(x => x.BillingName)
                .HasMaxLength(200);

            builder.Property(x => x.BillingAddress)
                .HasMaxLength(500);

            builder.Property(x => x.BillingEmail)
                .HasMaxLength(256);

            builder.Property(x => x.Phone)
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasMaxLength(256);

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(2000);

            builder.HasOne(x => x.PartnerType)
                .WithMany()
                .HasForeignKey(x => x.PartnerTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.PartnerTypeId);
        }

    }
}
