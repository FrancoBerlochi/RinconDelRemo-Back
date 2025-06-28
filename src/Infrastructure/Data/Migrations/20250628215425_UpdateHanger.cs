using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHanger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hangers_Kayaks_KayakId",
                table: "Hangers");

            migrationBuilder.DropIndex(
                name: "IX_Hangers_KayakId",
                table: "Hangers");

            migrationBuilder.DropColumn(
                name: "KayakId",
                table: "Hangers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Kayaks",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Kayaks",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Kayaks",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Material",
                table: "Kayaks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Length",
                table: "Kayaks",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Kayaks",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Kayaks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Hangers",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Hangers_OwnerId",
                table: "Hangers",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hangers_Users_OwnerId",
                table: "Hangers",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hangers_Users_OwnerId",
                table: "Hangers");

            migrationBuilder.DropIndex(
                name: "IX_Hangers_OwnerId",
                table: "Hangers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Hangers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Kayaks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Kayaks",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Kayaks",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Kayaks",
                keyColumn: "Model",
                keyValue: null,
                column: "Model",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Kayaks",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Material",
                table: "Kayaks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Kayaks",
                keyColumn: "Length",
                keyValue: null,
                column: "Length",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Length",
                table: "Kayaks",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Kayaks",
                keyColumn: "Color",
                keyValue: null,
                column: "Color",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Kayaks",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Kayaks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KayakId",
                table: "Hangers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hangers_KayakId",
                table: "Hangers",
                column: "KayakId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hangers_Kayaks_KayakId",
                table: "Hangers",
                column: "KayakId",
                principalTable: "Kayaks",
                principalColumn: "Id");
        }
    }
}
