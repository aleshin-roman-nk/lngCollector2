using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThoughtzLand.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNodeIdFromCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nodeId",
                table: "FlashCards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "nodeId",
                table: "FlashCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
