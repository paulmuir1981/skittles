using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skittles.WebApi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class PluralTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Pub_OpponentId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Pub_VenueId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Holiday_Events_EventId",
                table: "Holiday");

            migrationBuilder.DropForeignKey(
                name: "FK_Holiday_Players_PlayerId",
                table: "Holiday");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pub",
                table: "Pub");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Holiday",
                table: "Holiday");

            migrationBuilder.RenameTable(
                name: "Pub",
                newName: "Pubs");

            migrationBuilder.RenameTable(
                name: "Holiday",
                newName: "Holidays");

            migrationBuilder.RenameIndex(
                name: "IX_Pub_Name",
                table: "Pubs",
                newName: "IX_Pubs_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Holiday_EventId",
                table: "Holidays",
                newName: "IX_Holidays_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pubs",
                table: "Pubs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Holidays",
                table: "Holidays",
                columns: new[] { "PlayerId", "EventId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Pubs_OpponentId",
                table: "Events",
                column: "OpponentId",
                principalTable: "Pubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Pubs_VenueId",
                table: "Events",
                column: "VenueId",
                principalTable: "Pubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Events_EventId",
                table: "Holidays",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Players_PlayerId",
                table: "Holidays",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Pubs_OpponentId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Pubs_VenueId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Events_EventId",
                table: "Holidays");

            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Players_PlayerId",
                table: "Holidays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pubs",
                table: "Pubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Holidays",
                table: "Holidays");

            migrationBuilder.RenameTable(
                name: "Pubs",
                newName: "Pub");

            migrationBuilder.RenameTable(
                name: "Holidays",
                newName: "Holiday");

            migrationBuilder.RenameIndex(
                name: "IX_Pubs_Name",
                table: "Pub",
                newName: "IX_Pub_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Holidays_EventId",
                table: "Holiday",
                newName: "IX_Holiday_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pub",
                table: "Pub",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Holiday",
                table: "Holiday",
                columns: new[] { "PlayerId", "EventId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Pub_OpponentId",
                table: "Events",
                column: "OpponentId",
                principalTable: "Pub",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Pub_VenueId",
                table: "Events",
                column: "VenueId",
                principalTable: "Pub",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Holiday_Events_EventId",
                table: "Holiday",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holiday_Players_PlayerId",
                table: "Holiday",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
