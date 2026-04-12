using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skittles.WebApi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class PlayerIdGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints that reference Players.Id
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Players_PlayerId",
                table: "Scores");

            migrationBuilder.DropForeignKey(
                name: "FK_Holiday_Players_PlayerId",
                table: "Holiday");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Players_PlayerId",
                table: "Drivers");

            // Drop primary key constraints on dependent tables
            migrationBuilder.DropPrimaryKey(
                name: "PK_Scores",
                table: "Scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Holiday",
                table: "Holiday");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers");

            // Drop primary key on Players
            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            // Store the old Id as PlayerId before dropping
            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.Sql("UPDATE Players SET PlayerId = Id");

            // Drop the old Id column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Players");

            // Add new long Id with IDENTITY
            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Players",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            // Make PlayerId not nullable
            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerId",
                table: "Players",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            // Recreate primary key on Players
            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            // Create mapping table for old Guid to new long values
            migrationBuilder.Sql(@"
                CREATE TABLE #PlayerIdMapping (
                    OldId uniqueidentifier,
                    NewId bigint
                );
                INSERT INTO #PlayerIdMapping (OldId, NewId)
                SELECT PlayerId, Id FROM Players;
            ");

            // Create temporary bigint columns in dependent tables
            migrationBuilder.AddColumn<long>(
                name: "PlayerId_Temp",
                table: "Scores",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PlayerId_Temp",
                table: "Holiday",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PlayerId_Temp",
                table: "Drivers",
                type: "bigint",
                nullable: true);

            // Populate temporary columns with mapped values
            migrationBuilder.Sql(@"
                UPDATE Scores
                SET PlayerId_Temp = pm.NewId
                FROM Scores s
                INNER JOIN #PlayerIdMapping pm ON s.PlayerId = pm.OldId;
            ");

            migrationBuilder.Sql(@"
                UPDATE Holiday
                SET PlayerId_Temp = pm.NewId
                FROM Holiday h
                INNER JOIN #PlayerIdMapping pm ON h.PlayerId = pm.OldId;
            ");

            migrationBuilder.Sql(@"
                UPDATE Drivers
                SET PlayerId_Temp = pm.NewId
                FROM Drivers d
                INNER JOIN #PlayerIdMapping pm ON d.PlayerId = pm.OldId;
            ");

            // Drop the old Guid PlayerId columns
            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Holiday");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Drivers");

            // Rename temporary columns to PlayerId
            migrationBuilder.RenameColumn(
                name: "PlayerId_Temp",
                table: "Scores",
                newName: "PlayerId");

            migrationBuilder.RenameColumn(
                name: "PlayerId_Temp",
                table: "Holiday",
                newName: "PlayerId");

            migrationBuilder.RenameColumn(
                name: "PlayerId_Temp",
                table: "Drivers",
                newName: "PlayerId");

            // Alter columns to not nullable
            migrationBuilder.AlterColumn<long>(
                name: "PlayerId",
                table: "Scores",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PlayerId",
                table: "Holiday",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PlayerId",
                table: "Drivers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            // Drop mapping table
            migrationBuilder.Sql("DROP TABLE #PlayerIdMapping");

            // Recreate primary keys on dependent tables
            migrationBuilder.AddPrimaryKey(
                name: "PK_Scores",
                table: "Scores",
                columns: new[] { "PlayerId", "LegId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Holiday",
                table: "Holiday",
                columns: new[] { "PlayerId", "EventId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers",
                columns: new[] { "PlayerId", "EventId" });

            // Recreate foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Players_PlayerId",
                table: "Scores",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holiday_Players_PlayerId",
                table: "Holiday",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Players_PlayerId",
                table: "Drivers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Players_PlayerId",
                table: "Scores");

            migrationBuilder.DropForeignKey(
                name: "FK_Holiday_Players_PlayerId",
                table: "Holiday");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Players_PlayerId",
                table: "Drivers");

            // Drop primary keys on dependent tables
            migrationBuilder.DropPrimaryKey(
                name: "PK_Scores",
                table: "Scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Holiday",
                table: "Holiday");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers");

            // Drop primary key on Players
            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            // Create temporary Guid columns in dependent tables
            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId_Temp",
                table: "Scores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId_Temp",
                table: "Holiday",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId_Temp",
                table: "Drivers",
                type: "uniqueidentifier",
                nullable: true);

            // Populate temporary columns by looking up original Guid values from Players
            migrationBuilder.Sql(@"
                UPDATE Scores
                SET PlayerId_Temp = p.PlayerId
                FROM Scores s
                INNER JOIN Players p ON s.PlayerId = p.Id;
            ");

            migrationBuilder.Sql(@"
                UPDATE Holiday
                SET PlayerId_Temp = p.PlayerId
                FROM Holiday h
                INNER JOIN Players p ON h.PlayerId = p.Id;
            ");

            migrationBuilder.Sql(@"
                UPDATE Drivers
                SET PlayerId_Temp = p.PlayerId
                FROM Drivers d
                INNER JOIN Players p ON d.PlayerId = p.Id;
            ");

            // Drop the new long PlayerId columns
            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Holiday");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Drivers");

            // Rename temporary columns back to PlayerId
            migrationBuilder.RenameColumn(
                name: "PlayerId_Temp",
                table: "Scores",
                newName: "PlayerId");

            migrationBuilder.RenameColumn(
                name: "PlayerId_Temp",
                table: "Holiday",
                newName: "PlayerId");

            migrationBuilder.RenameColumn(
                name: "PlayerId_Temp",
                table: "Drivers",
                newName: "PlayerId");

            // Alter columns to not nullable
            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerId",
                table: "Scores",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerId",
                table: "Holiday",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerId",
                table: "Drivers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            // Drop the new Id column from Players
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Players");

            // Rename PlayerId back to Id
            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Players",
                newName: "Id");

            // Recreate primary key on Players
            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            // Recreate primary keys on dependent tables
            migrationBuilder.AddPrimaryKey(
                name: "PK_Scores",
                table: "Scores",
                columns: new[] { "PlayerId", "LegId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Holiday",
                table: "Holiday",
                columns: new[] { "PlayerId", "EventId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers",
                columns: new[] { "PlayerId", "EventId" });

            // Recreate foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Players_PlayerId",
                table: "Scores",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holiday_Players_PlayerId",
                table: "Holiday",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Players_PlayerId",
                table: "Drivers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
