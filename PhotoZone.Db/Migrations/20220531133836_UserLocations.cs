using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoZone.Db.Migrations
{
    public partial class UserLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscribes_Users_UserId",
                table: "Subscribes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locations_LocationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LocationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Subscribes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriberId",
                table: "Subscribes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribes_Users_UserId",
                table: "Subscribes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscribes_Users_UserId",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Subscribes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriberId",
                table: "Subscribes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LocationId",
                table: "Users",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribes_Users_UserId",
                table: "Subscribes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locations_LocationId",
                table: "Users",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
