using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThoughtzLand.Api.Migrations
{
    /// <inheritdoc />
    public partial class FlashCardIsExpressionTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCards_ThExpressions_expression1Id",
                table: "FlashCards");

            migrationBuilder.DropForeignKey(
                name: "FK_FlashCards_ThExpressions_expression2Id",
                table: "FlashCards");

            migrationBuilder.DropIndex(
                name: "IX_FlashCards_expression1Id",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "expression1Id",
                table: "FlashCards");

            migrationBuilder.RenameColumn(
                name: "expression2Id",
                table: "FlashCards",
                newName: "expressionId");

            migrationBuilder.RenameIndex(
                name: "IX_FlashCards_expression2Id",
                table: "FlashCards",
                newName: "IX_FlashCards_expressionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCards_ThExpressions_expressionId",
                table: "FlashCards",
                column: "expressionId",
                principalTable: "ThExpressions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCards_ThExpressions_expressionId",
                table: "FlashCards");

            migrationBuilder.RenameColumn(
                name: "expressionId",
                table: "FlashCards",
                newName: "expression2Id");

            migrationBuilder.RenameIndex(
                name: "IX_FlashCards_expressionId",
                table: "FlashCards",
                newName: "IX_FlashCards_expression2Id");

            migrationBuilder.AddColumn<int>(
                name: "expression1Id",
                table: "FlashCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FlashCards_expression1Id",
                table: "FlashCards",
                column: "expression1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCards_ThExpressions_expression1Id",
                table: "FlashCards",
                column: "expression1Id",
                principalTable: "ThExpressions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCards_ThExpressions_expression2Id",
                table: "FlashCards",
                column: "expression2Id",
                principalTable: "ThExpressions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
