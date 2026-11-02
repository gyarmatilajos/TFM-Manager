using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFM_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCompanyEmployeeSalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeContribution",
                table: "CompanyEmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "EmployerContribution",
                table: "CompanyEmployeeSalaries");

            migrationBuilder.AlterColumn<int>(
                name: "WorkTimeTypeId",
                table: "CompanyEmployeeSalaries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CompanyEmployeeSalary_GrossSalary",
                table: "CompanyEmployeeSalaries",
                sql: "[GrossSalary] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CompanyEmployeeSalary_NetSalary",
                table: "CompanyEmployeeSalaries",
                sql: "[NetSalary] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CompanyEmployeeSalary_TotalCompanyCost",
                table: "CompanyEmployeeSalaries",
                sql: "[TotalCompanyCost] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_CompanyEmployeeSalary_GrossSalary",
                table: "CompanyEmployeeSalaries");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CompanyEmployeeSalary_NetSalary",
                table: "CompanyEmployeeSalaries");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CompanyEmployeeSalary_TotalCompanyCost",
                table: "CompanyEmployeeSalaries");

            migrationBuilder.AlterColumn<int>(
                name: "WorkTimeTypeId",
                table: "CompanyEmployeeSalaries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EmployeeContribution",
                table: "CompanyEmployeeSalaries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EmployerContribution",
                table: "CompanyEmployeeSalaries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
