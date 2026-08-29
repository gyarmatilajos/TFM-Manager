using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFM_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PartnerCompanies_PartnerId_CompanyId_ServiceTypeId",
                table: "PartnerCompanies");

            migrationBuilder.DropIndex(
                name: "IX_CompanyEmployeeSalaries_CompanyEmployeeId",
                table: "CompanyEmployeeSalaries");

            migrationBuilder.DropIndex(
                name: "IX_CompanyEmployees_CompanyId_EmployeeId",
                table: "CompanyEmployees");

            migrationBuilder.DropIndex(
                name: "IX_CompanyBankAccounts_CompanyId",
                table: "CompanyBankAccounts");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerCompanies_PartnerId_CompanyId_ServiceTypeId",
                table: "PartnerCompanies",
                columns: new[] { "PartnerId", "CompanyId", "ServiceTypeId" },
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployeeSalaries_CompanyEmployeeId",
                table: "CompanyEmployeeSalaries",
                column: "CompanyEmployeeId",
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployees_CompanyId_EmployeeId",
                table: "CompanyEmployees",
                columns: new[] { "CompanyId", "EmployeeId" },
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBankAccounts_CompanyId",
                table: "CompanyBankAccounts",
                column: "CompanyId",
                unique: true,
                filter: "[IsActive] = 1 AND [IsDefault] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PartnerCompanies_PartnerId_CompanyId_ServiceTypeId",
                table: "PartnerCompanies");

            migrationBuilder.DropIndex(
                name: "IX_CompanyEmployeeSalaries_CompanyEmployeeId",
                table: "CompanyEmployeeSalaries");

            migrationBuilder.DropIndex(
                name: "IX_CompanyEmployees_CompanyId_EmployeeId",
                table: "CompanyEmployees");

            migrationBuilder.DropIndex(
                name: "IX_CompanyBankAccounts_CompanyId",
                table: "CompanyBankAccounts");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerCompanies_PartnerId_CompanyId_ServiceTypeId",
                table: "PartnerCompanies",
                columns: new[] { "PartnerId", "CompanyId", "ServiceTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployeeSalaries_CompanyEmployeeId",
                table: "CompanyEmployeeSalaries",
                column: "CompanyEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployees_CompanyId_EmployeeId",
                table: "CompanyEmployees",
                columns: new[] { "CompanyId", "EmployeeId" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBankAccounts_CompanyId",
                table: "CompanyBankAccounts",
                column: "CompanyId");
        }
    }
}
