using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealesApi.Migrations
{
    public partial class RemovedLinkPropertyWithTableWhatsSpecial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Property__WhatsS__778AC167",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_WhatsSpecialId",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "WhatsSpecialId",
                table: "Property");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WhatsSpecialId",
                table: "Property",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Property_WhatsSpecialId",
                table: "Property",
                column: "WhatsSpecialId");

            migrationBuilder.AddForeignKey(
                name: "FK__Property__WhatsS__778AC167",
                table: "Property",
                column: "WhatsSpecialId",
                principalTable: "WhatsSpecial",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
