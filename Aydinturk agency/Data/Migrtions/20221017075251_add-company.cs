using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aydinturk_agency.Data.Migrtions
{
    public partial class addcompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Flights");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_CompanyId",
                table: "Flights",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Companies_CompanyId",
                table: "Flights",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Companies_CompanyId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Flights_CompanyId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Flights");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
