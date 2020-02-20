using Microsoft.EntityFrameworkCore.Migrations;

namespace PDKSWebServer.Migrations
{
    public partial class ChangedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_AuthorID",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Categories_CategoryID",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizedUsers_Users_UserID",
                table: "AuthorizedUsers");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "AuthorizedUsers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorizedUsers_UserID",
                table: "AuthorizedUsers",
                newName: "IX_AuthorizedUsers_UserId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Articles",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Articles",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Articles",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_CategoryID",
                table: "Articles",
                newName: "IX_Articles_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AuthorID",
                table: "Articles",
                newName: "IX_Articles_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizedUsers_Users_UserId",
                table: "AuthorizedUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_AuthorId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizedUsers_Users_UserId",
                table: "AuthorizedUsers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AuthorizedUsers",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorizedUsers_UserId",
                table: "AuthorizedUsers",
                newName: "IX_AuthorizedUsers_UserID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Articles",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Articles",
                newName: "AuthorID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Articles",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                newName: "IX_Articles_CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                newName: "IX_Articles_AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_AuthorID",
                table: "Articles",
                column: "AuthorID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Categories_CategoryID",
                table: "Articles",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizedUsers_Users_UserID",
                table: "AuthorizedUsers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
