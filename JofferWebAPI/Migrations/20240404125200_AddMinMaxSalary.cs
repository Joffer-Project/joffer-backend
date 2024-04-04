using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JofferWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMinMaxSalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "JobOffers",
                newName: "MinSalary");

            migrationBuilder.AddColumn<int>(
                name: "MaxSalary",
                table: "JobOffers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxSalary",
                table: "JobOffers");

            migrationBuilder.RenameColumn(
                name: "MinSalary",
                table: "JobOffers",
                newName: "Salary");
        }
    }
}
