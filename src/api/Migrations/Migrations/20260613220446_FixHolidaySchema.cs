using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skittles.WebApi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class FixHolidaySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Events_EventId",
                table: "Holidays");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_EventId",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Holidays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Holidays",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_EventId",
                table: "Holidays",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Events_EventId",
                table: "Holidays",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
