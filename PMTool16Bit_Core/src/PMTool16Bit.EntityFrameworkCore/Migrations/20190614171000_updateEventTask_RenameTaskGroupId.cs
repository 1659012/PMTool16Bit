using Microsoft.EntityFrameworkCore.Migrations;

namespace PMTool16Bit.Migrations
{
    public partial class updateEventTask_RenameTaskGroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvenTasks_TaskGroups_GroupTaskId",
                table: "EvenTasks");

            migrationBuilder.RenameColumn(
                name: "GroupTaskId",
                table: "EvenTasks",
                newName: "TaskGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_EvenTasks_GroupTaskId",
                table: "EvenTasks",
                newName: "IX_EvenTasks_TaskGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvenTasks_TaskGroups_TaskGroupId",
                table: "EvenTasks",
                column: "TaskGroupId",
                principalTable: "TaskGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvenTasks_TaskGroups_TaskGroupId",
                table: "EvenTasks");

            migrationBuilder.RenameColumn(
                name: "TaskGroupId",
                table: "EvenTasks",
                newName: "GroupTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_EvenTasks_TaskGroupId",
                table: "EvenTasks",
                newName: "IX_EvenTasks_GroupTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvenTasks_TaskGroups_GroupTaskId",
                table: "EvenTasks",
                column: "GroupTaskId",
                principalTable: "TaskGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
