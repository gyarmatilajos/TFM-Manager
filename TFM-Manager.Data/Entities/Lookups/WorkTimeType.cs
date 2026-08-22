using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Data.Entities.Identity;

namespace TFM_Manager.Data.Entities.Lookups
{
    public class WorkTimeType : IEntityTypeConfiguration<WorkTimeType>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal DailyHours { get; set; }
        public bool IsActive { get; set; } = true;

        public void Configure(EntityTypeBuilder<WorkTimeType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.DailyHours)
                .IsRequired()
                .HasColumnType("decimal(4,2)");

            builder.Property(x => x.IsActive)
                .IsRequired();
        }
    }

}
