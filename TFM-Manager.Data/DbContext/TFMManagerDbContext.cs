using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TFM_Manager.Data.Entities.Companies;
using TFM_Manager.Data.Entities.Documents;
using TFM_Manager.Data.Entities.Employees;
using TFM_Manager.Data.Entities.Identity;
using TFM_Manager.Data.Entities.Lookups;
using TFM_Manager.Data.Entities.Partners;
using TFM_Manager.Data.Seed;

namespace TFM_Manager.Data.DbContext
{
    public class TfmManagerDbContext : IdentityUserContext<AppllicationUser, int>
    {
        public TfmManagerDbContext(DbContextOptions<TfmManagerDbContext> options)
            : base(options)
        {
        }

        // Companies
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyBankAccount> CompanyBankAccounts { get; set; }
        public DbSet<CompanyAccountingContact> CompanyAccountingContacts { get; set; }

        // Partners
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PartnerLocation> PartnerLocations { get; set; }
        public DbSet<PartnerContact> PartnerContacts { get; set; }
        public DbSet<PartnerCompany> PartnerCompanies { get; set; }

        // Employees
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CompanyEmployee> CompanyEmployees { get; set; }
        public DbSet<CompanyEmployeeSalary> CompanyEmployeeSalaries { get; set; }

        // Documents
        public DbSet<CompanyDocument> CompanyDocuments { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public DbSet<PartnerDocument> PartnerDocuments { get; set; }
        public DbSet<PartnerLocationDocument> PartnerLocationDocuments { get; set; }
        public DbSet<PartnerContactDocument> PartnerContactDocuments { get; set; }

        // Lookups
        public DbSet<PartnerType> PartnerTypes { get; set; }
        public DbSet<PartnerLocationType> PartnerLocationTypes { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
        public DbSet<WorkTimeType> WorkTimeTypes { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }

        // Permissions
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<RoleModulePermission> RoleModulePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TfmManagerDbContext).Assembly);

            LookupDataSeeder.Seed(modelBuilder);
        }
    }

}
