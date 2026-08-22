using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Data.Entities.Documents;
using TFM_Manager.Data.Entities.Lookups;

namespace TFM_Manager.Data.Entities.Partners
{
    public class PartnerContact : IEntityTypeConfiguration<PartnerContact>
    {
        public int Id { get; set; }

        public int PartnerId { get; set; }
        public Partner Partner { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

        public int ContactTypeId { get; set; }
        public ContactType ContactType { get; set; } = null!;

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        public ICollection<PartnerContactDocument> Documents { get; set; } = new List<PartnerContactDocument>();

        public void Configure(EntityTypeBuilder<PartnerContact> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PartnerId)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.ContactTypeId)
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasMaxLength(256);

            builder.Property(x => x.Position)
                .HasMaxLength(150);

            builder.Property(x => x.IsPrimary)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(2000);

            builder.HasOne(x => x.Partner)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.PartnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ContactType)
                .WithMany()
                .HasForeignKey(x => x.ContactTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.PartnerId);
            builder.HasIndex(x => x.ContactTypeId);
        }

    }
}
