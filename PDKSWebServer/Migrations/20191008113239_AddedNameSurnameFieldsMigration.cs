﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace PDKSWebServer.Migrations
{
    public partial class AddedNameSurnameFieldsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Users");
        }
    }
}
