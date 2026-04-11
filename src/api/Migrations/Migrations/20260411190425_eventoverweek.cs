using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skittles.WebApi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Eventoverweek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Weeks_WeekId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Holiday_Weeks_WeekId",
                table: "Holiday");

            migrationBuilder.DropForeignKey(
                name: "FK_Legs_Weeks_WeekId",
                table: "Legs");

            migrationBuilder.DropTable(
                name: "Weeks");

            migrationBuilder.RenameColumn(
                name: "WeekId",
                table: "Legs",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Legs_WeekId",
                table: "Legs",
                newName: "IX_Legs_EventId");

            migrationBuilder.RenameColumn(
                name: "WeekId",
                table: "Holiday",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Holiday_WeekId",
                table: "Holiday",
                newName: "IX_Holiday_EventId");

            migrationBuilder.RenameColumn(
                name: "WeekId",
                table: "Drivers",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_WeekId",
                table: "Drivers",
                newName: "IX_Drivers_EventId");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    EventType = table.Column<byte>(type: "tinyint", nullable: false),
                    IsAway = table.Column<bool>(type: "bit", nullable: false),
                    Opponent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_SeasonId",
                table: "Events",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Events_EventId",
                table: "Drivers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holiday_Events_EventId",
                table: "Holiday",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Legs_Events_EventId",
                table: "Legs",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Events_EventId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Holiday_Events_EventId",
                table: "Holiday");

            migrationBuilder.DropForeignKey(
                name: "FK_Legs_Events_EventId",
                table: "Legs");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Legs",
                newName: "WeekId");

            migrationBuilder.RenameIndex(
                name: "IX_Legs_EventId",
                table: "Legs",
                newName: "IX_Legs_WeekId");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Holiday",
                newName: "WeekId");

            migrationBuilder.RenameIndex(
                name: "IX_Holiday_EventId",
                table: "Holiday",
                newName: "IX_Holiday_WeekId");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Drivers",
                newName: "WeekId");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_EventId",
                table: "Drivers",
                newName: "IX_Drivers_WeekId");

            migrationBuilder.CreateTable(
                name: "Weeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    IsAway = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Number = table.Column<byte>(type: "tinyint", nullable: false),
                    Opponent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeekType = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weeks_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weeks_SeasonId",
                table: "Weeks",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Weeks_WeekId",
                table: "Drivers",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holiday_Weeks_WeekId",
                table: "Holiday",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Legs_Weeks_WeekId",
                table: "Legs",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
