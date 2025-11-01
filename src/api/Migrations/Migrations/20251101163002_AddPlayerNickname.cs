using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skittles.WebApi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerNickname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // First add the column allowing nulls
            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                schema: "skittles",
                table: "Players",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            // Update existing records to set Nickname equal to Name
            migrationBuilder.Sql("UPDATE skittles.Players SET Nickname = Name WHERE Nickname IS NULL");

            // Now make the column non-nullable
            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                schema: "skittles",
                table: "Players",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nickname",
                schema: "skittles",
                table: "Players");
        }
    }
}
