using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaborExchange.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTables3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ApplicantSpecialties",
                columns: table => new
                {
                    ApplicantsId = table.Column<int>(type: "integer", nullable: false),
                    SpecialtiesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantSpecialties", x => new { x.ApplicantsId, x.SpecialtiesId });
                    table.ForeignKey(
                        name: "FK_ApplicantSpecialties_Applicants_ApplicantsId",
                        column: x => x.ApplicantsId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantSpecialties_Specialities_SpecialtiesId",
                        column: x => x.SpecialtiesId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantSpecialties_SpecialtiesId",
                table: "ApplicantSpecialties",
                column: "SpecialtiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantSpecialties");

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
    }
}
