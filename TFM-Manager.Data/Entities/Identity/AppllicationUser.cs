using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Data.Entities.Employees;
using TFM_Manager.Data.Entities.Lookups;

namespace TFM_Manager.Data.Entities.Identity
{
    public class AppllicationUser : IdentityUser<int>, IEntityTypeConfiguration<AppllicationUser>
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public void Configure(EntityTypeBuilder<AppllicationUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmployeeId)
                .IsRequired();

            builder.Property(x => x.UserRoleId)
                .IsRequired();

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.NormalizedEmail)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.NormalizedUserName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.HasOne(x => x.Employee)
                .WithOne(x => x.ApplicationUser)
                .HasForeignKey<AppllicationUser>(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UserRole)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.UserRoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.EmployeeId)
                .IsUnique();

            builder.HasIndex(x => x.NormalizedEmail)
                .IsUnique();

        }
    }
}
