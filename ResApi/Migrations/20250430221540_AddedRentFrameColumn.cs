using Microsoft.EntityFrameworkCore.Migrations;

namespace RealesApi.Migrations
{
    public partial class AddedRentFrameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentFrame",
                table: "Property",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentFrame",
                table: "Property");
        }
    }
}
