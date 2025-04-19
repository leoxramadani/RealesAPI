using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealesApi.Migrations
{
    public partial class newChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaveProperty_Property_PropertyId",
                table: "SaveProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_SaveProperty_Users_UsersId",
                table: "SaveProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaveProperty",
                table: "SaveProperty");

            migrationBuilder.DropIndex(
                name: "IX_SaveProperty_UsersId",
                table: "SaveProperty");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "SaveProperty");

            migrationBuilder.RenameTable(
                name: "SaveProperty",
                newName: "SaveProperties");

            migrationBuilder.RenameIndex(
                name: "IX_SaveProperty_PropertyId",
                table: "SaveProperties",
                newName: "IX_SaveProperties_PropertyId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "SaveProperties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaveProperties",
                table: "SaveProperties",
                column: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "SaveProperties",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_SaveProperties_SellerId",
                table: "SaveProperties",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaveProperty_Property_PropertyId",
                table: "SaveProperties",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaveProperty_Users_UsersId",
                table: "SaveProperties",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaveProperty_Property_PropertyId",
                table: "SaveProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_SaveProperty_Users_UsersId",
                table: "SaveProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaveProperties",
                table: "SaveProperties");

            migrationBuilder.DropIndex(
                name: "IX_SaveProperties_SellerId",
                table: "SaveProperties");

            migrationBuilder.RenameTable(
                name: "SaveProperties",
                newName: "SaveProperty");

            migrationBuilder.RenameIndex(
                name: "IX_SaveProperties_PropertyId",
                table: "SaveProperty",
                newName: "IX_SaveProperty_PropertyId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "SaveProperty",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "SaveProperty",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaveProperty",
                table: "SaveProperty",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SaveProperty_UsersId",
                table: "SaveProperty",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaveProperty_Property_PropertyId",
                table: "SaveProperty",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaveProperty_Users_UsersId",
                table: "SaveProperty",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
