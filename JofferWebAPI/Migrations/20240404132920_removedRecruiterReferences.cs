using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JofferWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class removedRecruiterReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecruiterToJobOffers");

            migrationBuilder.DropTable(
                name: "Recruiters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recruiters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recruiters_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recruiters_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecruiterToJobOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobOfferId = table.Column<int>(type: "integer", nullable: false),
                    RecruiterId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruiterToJobOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecruiterToJobOffers_JobOffers_JobOfferId",
                        column: x => x.JobOfferId,
                        principalTable: "JobOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecruiterToJobOffers_Recruiters_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "Recruiters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recruiters_AccountId",
                table: "Recruiters",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiters_CompanyId",
                table: "Recruiters",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RecruiterToJobOffers_JobOfferId",
                table: "RecruiterToJobOffers",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_RecruiterToJobOffers_RecruiterId",
                table: "RecruiterToJobOffers",
                column: "RecruiterId");
        }
    }
}
