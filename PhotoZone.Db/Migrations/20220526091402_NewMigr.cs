using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoZone.Db.Migrations
{
    public partial class NewMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagesId",
                table: "Places");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImagesId",
                table: "Places",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
