using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Data.Entities.Lookups;

namespace TFM_Manager.Data.Seed
{
    public static class LookupDataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedPartnerTypes(modelBuilder);
            SeedPartnerLocationTypes(modelBuilder);
            SeedContactTypes(modelBuilder);
            SeedServiceTypes(modelBuilder);
            SeedEmployeeTypes(modelBuilder);
            SeedEmploymentTypes(modelBuilder);
            SeedWorkTimeTypes(modelBuilder);
            SeedDocumentTypes(modelBuilder);
            SeedUserRoles(modelBuilder);
            SeedModules(modelBuilder);
            SeedRoleModulePermissions(modelBuilder);
        }

        private static void SeedPartnerTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PartnerType>().HasData(
                new PartnerType { Id = 1, Name = "Társasház", IsActive = true },
                new PartnerType { Id = 2, Name = "Magánszemély", IsActive = true },
                new PartnerType { Id = 3, Name = "Gazdasági társaság", IsActive = true },
                new PartnerType { Id = 4, Name = "Intézmény", IsActive = true },
                new PartnerType { Id = 5, Name = "Egyéb", IsActive = true }
            );
        }

        private static void SeedPartnerLocationTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PartnerLocationType>().HasData(
                new PartnerLocationType { Id = 1, Name = "Épület", IsActive = true },
                new PartnerLocationType { Id = 2, Name = "Lépcsőház", IsActive = true },
                new PartnerLocationType { Id = 3, Name = "Garázs", IsActive = true },
                new PartnerLocationType { Id = 4, Name = "Porta", IsActive = true },
                new PartnerLocationType { Id = 5, Name = "Udvar / kert", IsActive = true },
                new PartnerLocationType { Id = 6, Name = "Tároló", IsActive = true },
                new PartnerLocationType { Id = 7, Name = "Gépészeti helyiség", IsActive = true },
                new PartnerLocationType { Id = 8, Name = "Hulladéktároló", IsActive = true },
                new PartnerLocationType { Id = 9, Name = "Egyéb", IsActive = true }
            );
        }

        private static void SeedContactTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactType>().HasData(
                new ContactType { Id = 1, Name = "Közös képviselő", IsActive = true },
                new ContactType { Id = 2, Name = "Számvizsgáló", IsActive = true },
                new ContactType { Id = 3, Name = "Pénzügyi kapcsolattartó", IsActive = true },
                new ContactType { Id = 4, Name = "Műszaki kapcsolattartó", IsActive = true },
                new ContactType { Id = 5, Name = "Gondnok", IsActive = true },
                new ContactType { Id = 6, Name = "Lakó", IsActive = true },
                new ContactType { Id = 7, Name = "Tulajdonos", IsActive = true },
                new ContactType { Id = 8, Name = "Irodai kapcsolattartó", IsActive = true },
                new ContactType { Id = 9, Name = "Egyéb", IsActive = true }
            );
        }

        private static void SeedServiceTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceType>().HasData(
                new ServiceType { Id = 1, Name = "Takarítás", IsActive = true },
                new ServiceType { Id = 2, Name = "Gondnokság", IsActive = true },
                new ServiceType { Id = 3, Name = "Kertészet", IsActive = true },
                new ServiceType { Id = 4, Name = "Portaszolgálat", IsActive = true },
                new ServiceType { Id = 5, Name = "Karbantartás", IsActive = true },
                new ServiceType { Id = 6, Name = "Eseti munka", IsActive = true },
                new ServiceType { Id = 7, Name = "Garázstakarítás", IsActive = true },
                new ServiceType { Id = 8, Name = "Hibaelhárítás", IsActive = true },
                new ServiceType { Id = 9, Name = "Egyéb", IsActive = true }
            );
        }

        private static void SeedEmployeeTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeType>().HasData(
                new EmployeeType { Id = 1, Name = "Normál dolgozó", IsActive = true },
                new EmployeeType { Id = 2, Name = "Képviselő", IsActive = true },
                new EmployeeType { Id = 3, Name = "Vezető", IsActive = true },
                new EmployeeType { Id = 4, Name = "Adminisztratív munkatárs", IsActive = true },
                new EmployeeType { Id = 5, Name = "Külsős", IsActive = true },
                new EmployeeType { Id = 6, Name = "Alvállalkozó", IsActive = true },
                new EmployeeType { Id = 7, Name = "Egyéb", IsActive = true }
            );
        }

        private static void SeedEmploymentTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmploymentType>().HasData(
                new EmploymentType { Id = 1, Name = "Munkaviszony", IsActive = true },
                new EmploymentType { Id = 2, Name = "Egyszerűsített foglalkoztatás", IsActive = true },
                new EmploymentType { Id = 3, Name = "Megbízási jogviszony", IsActive = true },
                new EmploymentType { Id = 4, Name = "Alvállalkozó", IsActive = true },
                new EmploymentType { Id = 5, Name = "Ügyvezető", IsActive = true },
                new EmploymentType { Id = 6, Name = "Tulajdonos", IsActive = true },
                new EmploymentType { Id = 7, Name = "Egyéb", IsActive = true }
            );
        }

        private static void SeedWorkTimeTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkTimeType>().HasData(
                new WorkTimeType { Id = 1, Name = "Napi 2 óra", DailyHours = 2.00m, IsActive = true },
                new WorkTimeType { Id = 2, Name = "Napi 4 óra", DailyHours = 4.00m, IsActive = true },
                new WorkTimeType { Id = 3, Name = "Napi 6 óra", DailyHours = 6.00m, IsActive = true },
                new WorkTimeType { Id = 4, Name = "Napi 8 óra", DailyHours = 8.00m, IsActive = true },
                new WorkTimeType { Id = 5, Name = "Egyéb", DailyHours = 0.00m, IsActive = true }
            );
        }

        private static void SeedDocumentTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentType>().HasData(
                new DocumentType { Id = 1, Name = "Szerződés", IsActive = true },
                new DocumentType { Id = 2, Name = "Megrendelő", IsActive = true },
                new DocumentType { Id = 3, Name = "Teljesítésigazolás", IsActive = true },
                new DocumentType { Id = 4, Name = "Jegyzőkönyv", IsActive = true },
                new DocumentType { Id = 5, Name = "Cégkivonat", IsActive = true },
                new DocumentType { Id = 6, Name = "Aláírási címpéldány", IsActive = true },
                new DocumentType { Id = 7, Name = "NAV dokumentum", IsActive = true },
                new DocumentType { Id = 8, Name = "Banki dokumentum", IsActive = true },
                new DocumentType { Id = 9, Name = "Személyi igazolvány", IsActive = true },
                new DocumentType { Id = 10, Name = "Lakcímkártya", IsActive = true },
                new DocumentType { Id = 11, Name = "Adókártya", IsActive = true },
                new DocumentType { Id = 12, Name = "TAJ kártya", IsActive = true },
                new DocumentType { Id = 13, Name = "Munkaszerződés", IsActive = true },
                new DocumentType { Id = 14, Name = "Orvosi alkalmassági", IsActive = true },
                new DocumentType { Id = 15, Name = "Egyéb", IsActive = true }
            );
        }

        private static void SeedUserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, Name = "Dolgozó", IsActive = true },
                new UserRole { Id = 2, Name = "Supervisor", IsActive = true },
                new UserRole { Id = 3, Name = "Vezető", IsActive = true },
                new UserRole { Id = 4, Name = "Admin", IsActive = true }
            );
        }

        private static void SeedModules(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>().HasData(
                new Module { Id = 1, Name = "Alapadatok", IsActive = true },
                new Module { Id = 2, Name = "Tasks", IsActive = true },
                new Module { Id = 3, Name = "Számlázás", IsActive = true },
                new Module { Id = 4, Name = "Riportok", IsActive = true },
                new Module { Id = 5, Name = "User kezelés", IsActive = true },
                new Module { Id = 6, Name = "Törzsadatok", IsActive = true }
            );
        }

        private static void SeedRoleModulePermissions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleModulePermission>().HasData(
                new RoleModulePermission
                {
                    Id = 1,
                    UserRoleId = 1,
                    ModuleId = 2,
                    CanView = true,
                    CanCreate = true,
                    CanEdit = false,
                    CanDelete = false
                },
                new RoleModulePermission
                {
                    Id = 2,
                    UserRoleId = 2,
                    ModuleId = 1,
                    CanView = true,
                    CanCreate = true,
                    CanEdit = true,
                    CanDelete = false
                },
                new RoleModulePermission
                {
                    Id = 3,
                    UserRoleId = 2,
                    ModuleId = 2,
                    CanView = true,
                    CanCreate = true,
                    CanEdit = true,
                    CanDelete = false
                },
                new RoleModulePermission
                {
                    Id = 4,
                    UserRoleId = 3,
                    ModuleId = 1,
                    CanView = true,
                    CanCreate = true,
                    CanEdit = true,
                    CanDelete = false
                },
                new RoleModulePermission
                {
                    Id = 5,
                    UserRoleId = 3,
                    ModuleId = 2,
                    CanView = true,
                    CanCreate = true,
                    CanEdit = true,
                    CanDelete = false
                },
                new RoleModulePermission
                {
                    Id = 6,
                    UserRoleId = 3,
                    ModuleId = 3,
                    CanView = true,
                    CanCreate = true,
                    CanEdit = true,
                    CanDelete = false
                },
                new RoleModulePermission
                {
                    Id = 7,
                    UserRoleId = 3,
                    ModuleId = 4,
                    CanView = true,
                    CanCreate = false,
                    CanEdit = false,
                    CanDelete = false
                },
                new RoleModulePermission
                {
                    Id = 8,
                    UserRoleId = 3,
                    ModuleId = 6,
                    CanView = true,
                    CanCreate = false,
                    CanEdit = false,
                    CanDelete = false
                },
                new RoleModulePermission
                {
                    Id = 9,
                    UserRoleId = 4,
                    ModuleId = 1,
                    CanView = true,
                    CanCreate = true,
                    CanEdit = true,
                    CanDelete = false
                },
                new RoleModulePermission
                {
                    Id = 10,
                    UserRoleId = 4,
                    ModuleId = 2,
                    CanView = true,
                    CanCreate = true,
                    CanEdit = true,
                    CanDelete = false
                },
                new RoleModulePermission
                {
                    Id = 11,
                    UserRoleId = 4,
                    ModuleId = 3,
                    CanView = true,
                    CanCreate = true,
                    CanEdit = true,
                    CanDelete = false
                },
                new RoleModulePermission
                {
                    Id = 12,
                    UserRoleId = 4,
                    ModuleId = 4,
                    CanView = true,
                    CanCreate = false,
                    CanEdit = false,
                    CanDelete = false
                },
                new RoleModulePermission
                {
                    Id = 13,
                    UserRoleId = 4,
                    ModuleId = 5,
                    CanView = true,
                    CanCreate = true,
                    CanEdit = true,
                    CanDelete = false
                },
                new RoleModulePermission
                {
                    Id = 14,
                    UserRoleId = 4,
                    ModuleId = 6,
                    CanView = true,
                    CanCreate = true,
                    CanEdit = true,
                    CanDelete = false
                }
            );
        }

    }
}
