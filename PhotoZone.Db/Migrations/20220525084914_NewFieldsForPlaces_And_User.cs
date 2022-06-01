using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoZone.Db.Migrations
{
    public partial class NewFieldsForPlaces_And_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookLink",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GitHubLink",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstLink",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TikTokLink",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookLink",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GitHubLink",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InstLink",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TikTokLink",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Places");
        }
    }
}
