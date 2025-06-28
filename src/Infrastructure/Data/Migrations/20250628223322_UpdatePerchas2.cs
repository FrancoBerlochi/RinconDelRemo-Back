using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePerchas2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HangerId",
                table: "Kayaks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Kayaks_HangerId",
                table: "Kayaks",
                column: "HangerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kayaks_Hangers_HangerId",
                table: "Kayaks",
                column: "HangerId",
                principalTable: "Hangers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kayaks_Hangers_HangerId",
                table: "Kayaks");

            migrationBuilder.DropIndex(
                name: "IX_Kayaks_HangerId",
                table: "Kayaks");

            migrationBuilder.DropColumn(
                name: "HangerId",
                table: "Kayaks");
        }
    }
}
