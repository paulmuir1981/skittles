using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skittles.WebApi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Players",
                schema: "skittles",
                newName: "Players");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "skittles");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Players",
                newSchema: "skittles");
        }
    }
}
