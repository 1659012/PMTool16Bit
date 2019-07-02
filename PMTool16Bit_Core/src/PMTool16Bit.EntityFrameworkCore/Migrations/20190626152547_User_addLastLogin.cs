using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PMTool16Bit.Migrations
{
    public partial class User_addLastLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "AbpUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "AbpUsers");
        }
    }
}