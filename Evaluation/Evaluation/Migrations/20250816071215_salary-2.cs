using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evaluation.Migrations
{
    /// <inheritdoc />
    public partial class salary2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fixed_salry",
                table: "salaries",
                newName: "year");

            migrationBuilder.AddColumn<string>(
                name: "month",
                table: "salaries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "salary",
                table: "salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "month",
                table: "salaries");

            migrationBuilder.DropColumn(
                name: "salary",
                table: "salaries");

            migrationBuilder.RenameColumn(
                name: "year",
                table: "salaries",
                newName: "fixed_salry");
        }
    }
}
