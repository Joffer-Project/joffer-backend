using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JofferWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesAndIndustries2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "JobOfferRoles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferRoles_AccountId",
                table: "JobOfferRoles",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferRoles_RoleId",
                table: "JobOfferRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoles_AccountId",
                table: "AccountRoles",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoles_RoleId",
                table: "AccountRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Accounts_AccountId",
                table: "AccountRoles",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Role_RoleId",
                table: "AccountRoles",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferRoles_Accounts_AccountId",
                table: "JobOfferRoles",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferRoles_Role_RoleId",
                table: "JobOfferRoles",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Accounts_AccountId",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Role_RoleId",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferRoles_Accounts_AccountId",
                table: "JobOfferRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferRoles_Role_RoleId",
                table: "JobOfferRoles");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_JobOfferRoles_AccountId",
                table: "JobOfferRoles");

            migrationBuilder.DropIndex(
                name: "IX_JobOfferRoles_RoleId",
                table: "JobOfferRoles");

            migrationBuilder.DropIndex(
                name: "IX_AccountRoles_AccountId",
                table: "AccountRoles");

            migrationBuilder.DropIndex(
                name: "IX_AccountRoles_RoleId",
                table: "AccountRoles");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "JobOfferRoles");
        }
    }
}
