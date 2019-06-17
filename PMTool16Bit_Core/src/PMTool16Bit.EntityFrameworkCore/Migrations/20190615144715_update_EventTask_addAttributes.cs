using Microsoft.EntityFrameworkCore.Migrations;

namespace PMTool16Bit.Migrations
{
    public partial class update_EventTask_addAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "EvenTasks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMarked",
                table: "EvenTasks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "EvenTasks");

            migrationBuilder.DropColumn(
                name: "IsMarked",
                table: "EvenTasks");
        }
    }
}
