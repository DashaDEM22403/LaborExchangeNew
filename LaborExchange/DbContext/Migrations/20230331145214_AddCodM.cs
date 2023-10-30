using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaborExchange.Migrations
{
    /// <inheritdoc />
    public partial class AddCodM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Сode",
                table: "Specialities",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Сode",
                table: "Specialities");
        }
    }
}
