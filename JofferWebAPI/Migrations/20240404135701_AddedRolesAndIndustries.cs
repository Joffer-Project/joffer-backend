using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JofferWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesAndIndustries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Fields_FieldId",
                table: "JobOffers");

            migrationBuilder.DropTable(
                name: "AccountDiciplines");

            migrationBuilder.DropTable(
                name: "Diciplines");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.CreateTable(
                name: "AccountIndustries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IndustryId = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountIndustries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Industry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobOfferIndustries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IndustryId = table.Column<int>(type: "integer", nullable: false),
                    JobOfferId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOfferIndustries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobOfferRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    JobOfferId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOfferRoles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Industry_FieldId",
                table: "JobOffers",
                column: "FieldId",
                principalTable: "Industry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Industry_FieldId",
                table: "JobOffers");

            migrationBuilder.DropTable(
                name: "AccountIndustries");

            migrationBuilder.DropTable(
                name: "AccountRoles");

            migrationBuilder.DropTable(
                name: "Industry");

            migrationBuilder.DropTable(
                name: "JobOfferIndustries");

            migrationBuilder.DropTable(
                name: "JobOfferRoles");

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FieldId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diciplines_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountDiciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    DiciplineId = table.Column<int>(type: "integer", nullable: false),
                    FieldId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDiciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountDiciplines_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountDiciplines_Diciplines_DiciplineId",
                        column: x => x.DiciplineId,
                        principalTable: "Diciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountDiciplines_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountDiciplines_AccountId",
                table: "AccountDiciplines",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountDiciplines_DiciplineId",
                table: "AccountDiciplines",
                column: "DiciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountDiciplines_FieldId",
                table: "AccountDiciplines",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Diciplines_FieldId",
                table: "Diciplines",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Fields_FieldId",
                table: "JobOffers",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
