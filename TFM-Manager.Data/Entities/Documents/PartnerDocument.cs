using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Data.Entities.Identity;
using TFM_Manager.Data.Entities.Lookups;
using TFM_Manager.Data.Entities.Partners;

namespace TFM_Manager.Data.Entities.Documents
{
    public class PartnerDocument : IEntityTypeConfiguration<PartnerDocument>
    {
        public int Id { get; set; }

        public int PartnerId { get; set; }
        public Partner Partner { get; set; } = null!;

        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; } = null!;

        public string OriginalFileName { get; set; } = string.Empty;
        public string StoredFileName { get; set; } = string.Empty;
        public string RelativePath { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public string? MimeType { get; set; }
        public long? FileSize { get; set; }

        public int UploadedByUserId { get; set; }
        public AppllicationUser UploadedByUser { get; set; } = null!;

        public DateTime UploadedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        public void Configure(EntityTypeBuilder<PartnerDocument> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PartnerId)
                .IsRequired();

            builder.Property(x => x.DocumentTypeId)
                .IsRequired();

            builder.Property(x => x.OriginalFileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.StoredFileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.RelativePath)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.FileExtension)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.MimeType)
                .HasMaxLength(150);

            builder.Property(x => x.UploadedByUserId)
                .IsRequired();

            builder.Property(x => x.UploadedAt)
                .IsRequired()
                .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(2000);

            builder.HasOne(x => x.Partner)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.PartnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DocumentType)
                .WithMany()
                .HasForeignKey(x => x.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UploadedByUser)
                .WithMany()
                .HasForeignKey(x => x.UploadedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.PartnerId);
            builder.HasIndex(x => x.DocumentTypeId);
            builder.HasIndex(x => x.UploadedByUserId);
        }

    }
}
