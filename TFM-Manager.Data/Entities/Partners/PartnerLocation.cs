using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Data.Entities.Companies;
using TFM_Manager.Data.Entities.Documents;
using TFM_Manager.Data.Entities.Lookups;

namespace TFM_Manager.Data.Entities.Partners
{
    public class PartnerLocation : IEntityTypeConfiguration<PartnerLocation>
    {
        public int Id { get; set; }

        public int PartnerId { get; set; }
        public Partner Partner { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

        public int LocationTypeId { get; set; }
        public PartnerLocationType LocationType { get; set; } = null!;

        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        public ICollection<PartnerLocationDocument> Documents { get; set; } = new List<PartnerLocationDocument>();

        public void Configure(EntityTypeBuilder<PartnerLocation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PartnerId)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.LocationTypeId)
                .IsRequired();

            builder.Property(x => x.Address)
                .HasMaxLength(500);

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(2000);

            builder.HasOne(x => x.Partner)
                .WithMany(x => x.Locations)
                .HasForeignKey(x => x.PartnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.LocationType)
                .WithMany()
                .HasForeignKey(x => x.LocationTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.PartnerId);
            builder.HasIndex(x => x.LocationTypeId);
        }
    }

}
