using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Data.Entities.Companies;
using TFM_Manager.Data.Entities.Lookups;

namespace TFM_Manager.Data.Entities.Partners
{
    public class PartnerCompany : IEntityTypeConfiguration<PartnerCompany>
    {
        public int Id { get; set; }

        public int PartnerId { get; set; }
        public Partner Partner { get; set; } = null!;

        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; } = null!;

        public string? ContractNumber { get; set; }
        public DateOnly ContractStartDate { get; set; }
        public DateOnly? ContractEndDate { get; set; }
        public decimal? ContractFee { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        public void Configure(EntityTypeBuilder<PartnerCompany> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PartnerId)
                .IsRequired();

            builder.Property(x => x.CompanyId)
                .IsRequired();

            builder.Property(x => x.ServiceTypeId)
                .IsRequired();

            builder.Property(x => x.ContractNumber)
                .HasMaxLength(100);

            builder.Property(x => x.ContractStartDate)
                .IsRequired();

            builder.Property(x => x.ContractFee)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(2000);

            builder.HasOne(x => x.Partner)
                .WithMany(x => x.PartnerCompanies)
                .HasForeignKey(x => x.PartnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Company)
                .WithMany(x => x.PartnerCompanies)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ServiceType)
                .WithMany()
                .HasForeignKey(x => x.ServiceTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            

            builder.HasIndex(x => new
            {
                x.PartnerId,
                x.CompanyId,
                x.ServiceTypeId
            })
                .IsUnique()
                .HasFilter("[IsActive] = 1");
        }

    }
}
