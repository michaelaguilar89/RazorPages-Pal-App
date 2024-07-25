using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPages_Pal_App.Migrations
{
    /// <inheritdoc />
    public partial class updateStoreModelv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModificacionTime",
                table: "stores",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModificacionTime",
                table: "ResultStoreDto",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModificacionTime",
                table: "stores",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModificacionTime",
                table: "ResultStoreDto",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
