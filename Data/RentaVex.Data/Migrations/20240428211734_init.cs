using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaVex.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UnavailableDates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UnavailableDates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UnavailableDates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UnavailableDates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnavailableDates_IsDeleted",
                table: "UnavailableDates",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UnavailableDates_IsDeleted",
                table: "UnavailableDates");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UnavailableDates");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UnavailableDates");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UnavailableDates");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UnavailableDates");
        }
    }
}
