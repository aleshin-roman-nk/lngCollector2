using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThoughtzLand.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFlashCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastExam",
                table: "FlashCards");

            migrationBuilder.RenameColumn(
                name: "SpacedRepetitionBoxCellId",
                table: "FlashCards",
                newName: "boxCellNo");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Terrains",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "Terrains");

            migrationBuilder.RenameColumn(
                name: "boxCellNo",
                table: "FlashCards",
                newName: "SpacedRepetitionBoxCellId");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastExam",
                table: "FlashCards",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
