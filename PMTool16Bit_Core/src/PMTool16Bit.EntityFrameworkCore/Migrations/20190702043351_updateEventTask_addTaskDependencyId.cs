using Microsoft.EntityFrameworkCore.Migrations;

namespace PMTool16Bit.Migrations
{
    public partial class updateEventTask_addTaskDependencyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskDependencyId",
                table: "EvenTasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskDependencyId",
                table: "EvenTasks");
        }
    }
}
