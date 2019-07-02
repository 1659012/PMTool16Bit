using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PMTool16Bit.Migrations
{
    public partial class updateProject_RenameTaskGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvenTasks_GroupTasks_GroupTaskId",
                table: "EvenTasks");

            migrationBuilder.DropTable(
                name: "GroupTasks");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Projects",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileIds",
                table: "Comments",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TaskGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    TaskGroupName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskGroups_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskGroups_ProjectId",
                table: "TaskGroups",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvenTasks_TaskGroups_GroupTaskId",
                table: "EvenTasks",
                column: "GroupTaskId",
                principalTable: "TaskGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvenTasks_TaskGroups_GroupTaskId",
                table: "EvenTasks");

            migrationBuilder.DropTable(
                name: "TaskGroups");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "FileIds",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GroupTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    GroupTaskName = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTasks_ProjectId",
                table: "GroupTasks",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvenTasks_GroupTasks_GroupTaskId",
                table: "EvenTasks",
                column: "GroupTaskId",
                principalTable: "GroupTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}