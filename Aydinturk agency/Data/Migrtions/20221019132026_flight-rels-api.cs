using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aydinturk_agency.Data.Migrtions
{
    public partial class flightrelsapi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "Sets",
                table: "Flights",
                newName: "ToId");

            migrationBuilder.AddColumn<int>(
                name: "FromId",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_FromId",
                table: "Flights",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ToId",
                table: "Flights",
                column: "ToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Destinations_FromId",
                table: "Flights",
                column: "FromId",
                principalTable: "Destinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Destinations_ToId",
                table: "Flights",
                column: "ToId",
                principalTable: "Destinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Destinations_FromId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Destinations_ToId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_FromId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_ToId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "ToId",
                table: "Flights",
                newName: "Sets");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
