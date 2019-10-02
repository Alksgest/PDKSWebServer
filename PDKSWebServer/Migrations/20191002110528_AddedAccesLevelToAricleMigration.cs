using Microsoft.EntityFrameworkCore.Migrations;

namespace PDKSWebServer.Migrations
{
    public partial class AddedAccesLevelToAricleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessLevel",
                table: "Articles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessLevel",
                table: "Articles");
        }
    }
}
