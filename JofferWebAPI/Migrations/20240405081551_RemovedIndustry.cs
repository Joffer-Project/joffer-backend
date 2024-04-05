using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JofferWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovedIndustry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Industry_FieldId",
                table: "JobOffers");

            migrationBuilder.DropIndex(
                name: "IX_JobOffers_FieldId",
                table: "JobOffers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JobOffers_FieldId",
                table: "JobOffers",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Industry_FieldId",
                table: "JobOffers",
                column: "FieldId",
                principalTable: "Industry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
