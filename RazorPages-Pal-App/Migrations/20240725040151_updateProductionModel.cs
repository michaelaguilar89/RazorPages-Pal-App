using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPages_Pal_App.Migrations
{
    /// <inheritdoc />
    public partial class updateProductionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productions_AspNetUsers_UserIdModification",
                table: "productions");

            migrationBuilder.AlterColumn<string>(
                name: "ModificacionTime",
                table: "ResultStoreDto",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UserIdModification",
                table: "productions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificacionTime",
                table: "productions",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_productions_AspNetUsers_UserIdModification",
                table: "productions",
                column: "UserIdModification",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productions_AspNetUsers_UserIdModification",
                table: "productions");

            migrationBuilder.AlterColumn<string>(
                name: "ModificacionTime",
                table: "ResultStoreDto",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIdModification",
                table: "productions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificacionTime",
                table: "productions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_productions_AspNetUsers_UserIdModification",
                table: "productions",
                column: "UserIdModification",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
