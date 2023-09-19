using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThoughtzLand.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddBoxCells : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoxCells",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cellName = table.Column<int>(type: "INTEGER", nullable: false),
                    nextIntervalInDays = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxCells", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxCells");
        }
    }
}
