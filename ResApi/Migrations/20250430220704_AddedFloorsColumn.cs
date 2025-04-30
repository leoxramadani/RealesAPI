using Microsoft.EntityFrameworkCore.Migrations;

namespace RealesApi.Migrations
{
    public partial class AddedFloorsColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Floors",
                table: "Property",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Floors",
                table: "Property");
        }
    }
}
