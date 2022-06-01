using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoZone.Db.Migrations
{
    public partial class SubscribersAndSubscribtions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubscribtionsId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriberId",
                table: "Subscribes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Subscribtions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscribtionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribtions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscribtions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscribtions_UserId",
                table: "Subscribtions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscribtions");

            migrationBuilder.DropColumn(
                name: "SubscribtionsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SubscriberId",
                table: "Subscribes");
        }
    }
}
