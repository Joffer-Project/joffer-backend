using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JofferWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedEvrythingLol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ReachByEmail",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ReachByPhone",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "ApplicantInterested",
                table: "JobOfferSwipes",
                newName: "TalentInterested");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "JobOfferSwipes",
                newName: "TalentId");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Accounts",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "ComapnyUrl",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Companies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image2Url",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3Url",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4Url",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image5Url",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstaGramUrl",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInUrl",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterUrl",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeUrl",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Talents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    AboutMe = table.Column<string>(type: "text", nullable: false),
                    SalaryMinimum = table.Column<int>(type: "integer", nullable: false),
                    EmploymentStatus = table.Column<string>(type: "text", nullable: false),
                    AvatarUrl = table.Column<string>(type: "text", nullable: true),
                    Image2Url = table.Column<string>(type: "text", nullable: true),
                    Image3Url = table.Column<string>(type: "text", nullable: true),
                    Image4Url = table.Column<string>(type: "text", nullable: true),
                    Image5Url = table.Column<string>(type: "text", nullable: true),
                    GitHubUrl = table.Column<string>(type: "text", nullable: true),
                    LinkedInUrl = table.Column<string>(type: "text", nullable: true),
                    MediumUrl = table.Column<string>(type: "text", nullable: true),
                    DribbleUrl = table.Column<string>(type: "text", nullable: true),
                    PersonalUrl = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Talents_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Talents_AccountId",
                table: "Talents",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Talents");

            migrationBuilder.DropColumn(
                name: "ComapnyUrl",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Image2Url",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Image3Url",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Image4Url",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Image5Url",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "InstaGramUrl",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LinkedInUrl",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TwitterUrl",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "YoutubeUrl",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "TalentInterested",
                table: "JobOfferSwipes",
                newName: "ApplicantInterested");

            migrationBuilder.RenameColumn(
                name: "TalentId",
                table: "JobOfferSwipes",
                newName: "ApplicantId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Accounts",
                newName: "Username");

            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "Companies",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReachByEmail",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ReachByPhone",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    AboutMe = table.Column<string>(type: "text", nullable: false),
                    Avatar = table.Column<byte[]>(type: "bytea", nullable: true),
                    EmploymentStatus = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SalaryMinimum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applicants_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_AccountId",
                table: "Applicants",
                column: "AccountId");
        }
    }
}
