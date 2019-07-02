using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PMTool16Bit.Migrations
{
    public partial class update_UserProfile_EventTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "EvenTasks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublishProfile",
                table: "AbpUsers",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "EvenTasks");

            migrationBuilder.DropColumn(
                name: "IsPublishProfile",
                table: "AbpUsers");
        }
    }
}