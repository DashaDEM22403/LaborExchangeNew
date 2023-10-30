using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaborExchange.Migrations
{
    /// <inheritdoc />
    public partial class AddCode2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Сode",
                table: "Specialities",
                newName: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Specialities",
                newName: "Сode");
        }
    }
}
