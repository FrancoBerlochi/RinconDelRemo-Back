using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class KayakUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Kayaks",
                newName: "Material");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Kayaks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDate",
                table: "Kayaks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Kayaks");

            migrationBuilder.DropColumn(
                name: "PublicationDate",
                table: "Kayaks");

            migrationBuilder.RenameColumn(
                name: "Material",
                table: "Kayaks",
                newName: "Type");
        }
    }
}
