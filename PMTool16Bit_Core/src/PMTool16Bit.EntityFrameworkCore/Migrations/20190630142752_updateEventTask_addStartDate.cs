using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PMTool16Bit.Migrations
{
    public partial class updateEventTask_addStartDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "EvenTasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "EvenTasks");
        }
    }
}