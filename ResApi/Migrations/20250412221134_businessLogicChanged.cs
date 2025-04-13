using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealesApi.Migrations
{
    public partial class businessLogicChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__PropertyO__Other__7B5B524B",
                table: "PropertyOtherImages");

            migrationBuilder.DropTable(
                name: "OtherImages");

            migrationBuilder.DropIndex(
                name: "IX_PropertyOtherImages_OtherImagesId",
                table: "PropertyOtherImages");

            migrationBuilder.DropColumn(
                name: "OtherImagesId",
                table: "PropertyOtherImages");

            migrationBuilder.DropColumn(
                name: "OtherImagesId",
                table: "Property");

            migrationBuilder.AddColumn<string>(
                name: "Base64stringImage",
                table: "PropertyOtherImages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64stringImage",
                table: "PropertyOtherImages");

            migrationBuilder.AddColumn<Guid>(
                name: "OtherImagesId",
                table: "PropertyOtherImages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OtherImagesId",
                table: "Property",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OtherImages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    base64stringImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherImages", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOtherImages_OtherImagesId",
                table: "PropertyOtherImages",
                column: "OtherImagesId");

            migrationBuilder.AddForeignKey(
                name: "FK__PropertyO__Other__7B5B524B",
                table: "PropertyOtherImages",
                column: "OtherImagesId",
                principalTable: "OtherImages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
