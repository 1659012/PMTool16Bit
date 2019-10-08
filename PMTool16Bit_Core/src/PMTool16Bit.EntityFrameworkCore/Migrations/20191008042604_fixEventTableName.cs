using Microsoft.EntityFrameworkCore.Migrations;

namespace PMTool16Bit.Migrations
{
    public partial class fixEventTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_EvenTasks_EventTaskId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_EvenTasks_TaskGroups_TaskGroupId",
                table: "EvenTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTaskMembers_EvenTasks_EventTaskId",
                table: "EventTaskMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_EvenTasks_EventTaskId",
                table: "Todos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvenTasks",
                table: "EvenTasks");

            migrationBuilder.RenameTable(
                name: "EvenTasks",
                newName: "EventTasks");

            migrationBuilder.RenameIndex(
                name: "IX_EvenTasks_TaskGroupId",
                table: "EventTasks",
                newName: "IX_EventTasks_TaskGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventTasks",
                table: "EventTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_EventTasks_EventTaskId",
                table: "Comments",
                column: "EventTaskId",
                principalTable: "EventTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTaskMembers_EventTasks_EventTaskId",
                table: "EventTaskMembers",
                column: "EventTaskId",
                principalTable: "EventTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTasks_TaskGroups_TaskGroupId",
                table: "EventTasks",
                column: "TaskGroupId",
                principalTable: "TaskGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_EventTasks_EventTaskId",
                table: "Todos",
                column: "EventTaskId",
                principalTable: "EventTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_EventTasks_EventTaskId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTaskMembers_EventTasks_EventTaskId",
                table: "EventTaskMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTasks_TaskGroups_TaskGroupId",
                table: "EventTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_EventTasks_EventTaskId",
                table: "Todos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventTasks",
                table: "EventTasks");

            migrationBuilder.RenameTable(
                name: "EventTasks",
                newName: "EvenTasks");

            migrationBuilder.RenameIndex(
                name: "IX_EventTasks_TaskGroupId",
                table: "EvenTasks",
                newName: "IX_EvenTasks_TaskGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvenTasks",
                table: "EvenTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_EvenTasks_EventTaskId",
                table: "Comments",
                column: "EventTaskId",
                principalTable: "EvenTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EvenTasks_TaskGroups_TaskGroupId",
                table: "EvenTasks",
                column: "TaskGroupId",
                principalTable: "TaskGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTaskMembers_EvenTasks_EventTaskId",
                table: "EventTaskMembers",
                column: "EventTaskId",
                principalTable: "EvenTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_EvenTasks_EventTaskId",
                table: "Todos",
                column: "EventTaskId",
                principalTable: "EvenTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
