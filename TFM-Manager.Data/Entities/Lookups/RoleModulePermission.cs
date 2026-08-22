using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TFM_Manager.Data.Entities.Lookups
{
    public class RoleModulePermission : IEntityTypeConfiguration<RoleModulePermission>
    {
        public int Id { get; set; }

        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; } = null!;

        public int ModuleId { get; set; }
        public Module Module { get; set; } = null!;

        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        public void Configure(EntityTypeBuilder<RoleModulePermission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserRoleId)
                .IsRequired();

            builder.Property(x => x.ModuleId)
                .IsRequired();

            builder.Property(x => x.CanView)
                .IsRequired();

            builder.Property(x => x.CanCreate)
                .IsRequired();

            builder.Property(x => x.CanEdit)
                .IsRequired();

            builder.Property(x => x.CanDelete)
                .IsRequired();

            builder.HasOne(x => x.UserRole)
                .WithMany(x => x.Permissions)
                .HasForeignKey(x => x.UserRoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Module)
                .WithMany(x => x.Permissions)
                .HasForeignKey(x => x.ModuleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.UserRoleId, x.ModuleId })
                .IsUnique();
        }

    }
}
