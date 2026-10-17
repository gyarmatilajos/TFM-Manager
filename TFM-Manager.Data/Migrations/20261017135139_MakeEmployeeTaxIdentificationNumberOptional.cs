using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFM_Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeEmployeeTaxIdentificationNumberOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_TaxIdentificationNumber",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "TaxIdentificationNumber",
                table: "Employees",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TaxIdentificationNumber",
                table: "Employees",
                column: "TaxIdentificationNumber",
                unique: true,
                filter: "[TaxIdentificationNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_TaxIdentificationNumber",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "TaxIdentificationNumber",
                table: "Employees",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TaxIdentificationNumber",
                table: "Employees",
                column: "TaxIdentificationNumber",
                unique: true);
        }
    }
}
