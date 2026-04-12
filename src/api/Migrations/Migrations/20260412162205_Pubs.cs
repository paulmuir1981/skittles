using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skittles.WebApi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Pubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Legs_EventId",
                table: "Legs");

            migrationBuilder.DropIndex(
                name: "IX_Events_SeasonId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LastDeleted",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LastDeletedBy",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LastUndeleted",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LastUndeletedBy",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsAway",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Opponent",
                table: "Events");

            migrationBuilder.AddColumn<Guid>(
                name: "OpponentId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VenueId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pub",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pub", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Legs_EventId_Number",
                table: "Legs",
                columns: new[] { "EventId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_OpponentId",
                table: "Events",
                column: "OpponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SeasonId_Description",
                table: "Events",
                columns: new[] { "SeasonId", "Description" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Pub_Name",
                table: "Pub",
                column: "Name",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Pub_OpponentId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Pub_VenueId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Pub");

            migrationBuilder.DropIndex(
                name: "IX_Legs_EventId_Number",
                table: "Legs");

            migrationBuilder.DropIndex(
                name: "IX_Events_OpponentId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_SeasonId_Description",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_VenueId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "OpponentId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "VenueId",
                table: "Events");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastDeleted",
                table: "Players",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastDeletedBy",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastUndeleted",
                table: "Players",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastUndeletedBy",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAway",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Opponent",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Legs_EventId",
                table: "Legs",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SeasonId",
                table: "Events",
                column: "SeasonId");
        }
    }
}
