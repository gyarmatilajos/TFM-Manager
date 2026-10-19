using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFM_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyEmployeeDateConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_CompanyEmployee_EndDate_After_StartDate",
                table: "CompanyEmployees",
                sql: "[EndDate] IS NULL OR[EndDate] >= [StartDate]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CompanyEmployee_EndDate_Required_When_Inactive",
                table: "CompanyEmployees",
                sql: "[IsActive] = 1 OR[EndDate] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_CompanyEmployee_EndDate_After_StartDate",
                table: "CompanyEmployees");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CompanyEmployee_EndDate_Required_When_Inactive",
                table: "CompanyEmployees");
        }
    }
}
