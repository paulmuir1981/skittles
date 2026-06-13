using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skittles.WebApi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class HolidayDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Events_EventId",
                table: "Holidays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Holidays",
                table: "Holidays");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "Holidays",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Holidays",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Holidays",
                table: "Holidays",
                columns: new[] { "PlayerId", "Date" });

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Events_EventId",
                table: "Holidays",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Events_EventId",
                table: "Holidays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Holidays",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Holidays");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "Holidays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Holidays",
                table: "Holidays",
                columns: new[] { "PlayerId", "EventId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Events_EventId",
                table: "Holidays",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
