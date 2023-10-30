using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaborExchange.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialities_Applicants_ApplicantId",
                table: "Specialities");

            migrationBuilder.DropIndex(
                name: "IX_Specialities_ApplicantId",
                table: "Specialities");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Specialities");

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "Applicants",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_SpecialityId",
                table: "Applicants",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_Specialities_SpecialityId",
                table: "Applicants",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_Specialities_SpecialityId",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_SpecialityId",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "Applicants");

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "Specialities",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialities_ApplicantId",
                table: "Specialities",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialities_Applicants_ApplicantId",
                table: "Specialities",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id");
        }
    }
}
