using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JofferWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesAndIndustries3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferRoles_Accounts_AccountId",
                table: "JobOfferRoles");

            migrationBuilder.DropIndex(
                name: "IX_JobOfferRoles_AccountId",
                table: "JobOfferRoles");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "JobOfferRoles");

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferRoles_JobOfferId",
                table: "JobOfferRoles",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferIndustries_IndustryId",
                table: "JobOfferIndustries",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferIndustries_JobOfferId",
                table: "JobOfferIndustries",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountIndustries_AccountId",
                table: "AccountIndustries",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountIndustries_IndustryId",
                table: "AccountIndustries",
                column: "IndustryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountIndustries_Accounts_AccountId",
                table: "AccountIndustries",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountIndustries_Industry_IndustryId",
                table: "AccountIndustries",
                column: "IndustryId",
                principalTable: "Industry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferIndustries_Industry_IndustryId",
                table: "JobOfferIndustries",
                column: "IndustryId",
                principalTable: "Industry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferIndustries_JobOffers_JobOfferId",
                table: "JobOfferIndustries",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferRoles_JobOffers_JobOfferId",
                table: "JobOfferRoles",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountIndustries_Accounts_AccountId",
                table: "AccountIndustries");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountIndustries_Industry_IndustryId",
                table: "AccountIndustries");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferIndustries_Industry_IndustryId",
                table: "JobOfferIndustries");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferIndustries_JobOffers_JobOfferId",
                table: "JobOfferIndustries");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferRoles_JobOffers_JobOfferId",
                table: "JobOfferRoles");

            migrationBuilder.DropIndex(
                name: "IX_JobOfferRoles_JobOfferId",
                table: "JobOfferRoles");

            migrationBuilder.DropIndex(
                name: "IX_JobOfferIndustries_IndustryId",
                table: "JobOfferIndustries");

            migrationBuilder.DropIndex(
                name: "IX_JobOfferIndustries_JobOfferId",
                table: "JobOfferIndustries");

            migrationBuilder.DropIndex(
                name: "IX_AccountIndustries_AccountId",
                table: "AccountIndustries");

            migrationBuilder.DropIndex(
                name: "IX_AccountIndustries_IndustryId",
                table: "AccountIndustries");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "JobOfferRoles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferRoles_AccountId",
                table: "JobOfferRoles",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferRoles_Accounts_AccountId",
                table: "JobOfferRoles",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
