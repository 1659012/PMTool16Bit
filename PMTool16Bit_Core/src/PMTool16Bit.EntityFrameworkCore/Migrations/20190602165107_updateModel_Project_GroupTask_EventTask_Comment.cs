using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PMTool16Bit.Migrations
{
    public partial class updateModel_Project_GroupTask_EventTask_Comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvenTasks_EventTables_EventTableId",
                table: "EvenTasks");

            migrationBuilder.DropTable(
                name: "EventTables");

            migrationBuilder.DropIndex(
                name: "IX_EvenTasks_EventTableId",
                table: "EvenTasks");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EventTableId",
                table: "EvenTasks");

            migrationBuilder.AddColumn<long>(
                name: "ProjectOwnerId",
                table: "Projects",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "GroupTaskId",
                table: "EvenTasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "AbpUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Comments",
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
                    EventTaskId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(maxLength: 500, nullable: true),
                    FileIds = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_EvenTasks_EventTaskId",
                        column: x => x.EventTaskId,
                        principalTable: "EvenTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTaskMembers",
                columns: table => new
                {
                    EventTaskId = table.Column<int>(nullable: false),
                    MemberId = table.Column<long>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTaskMembers", x => new { x.EventTaskId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_EventTaskMembers_EvenTasks_EventTaskId",
                        column: x => x.EventTaskId,
                        principalTable: "EvenTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTaskMembers_AbpUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTasks",
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
                    GroupTaskName = table.Column<string>(maxLength: 256, nullable: true)
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
                name: "IX_Projects_ProjectOwnerId",
                table: "Projects",
                column: "ProjectOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_EvenTasks_GroupTaskId",
                table: "EvenTasks",
                column: "GroupTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EventTaskId",
                table: "Comments",
                column: "EventTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTaskMembers_MemberId",
                table: "EventTaskMembers",
                column: "MemberId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AbpUsers_ProjectOwnerId",
                table: "Projects",
                column: "ProjectOwnerId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvenTasks_GroupTasks_GroupTaskId",
                table: "EvenTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AbpUsers_ProjectOwnerId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "EventTaskMembers");

            migrationBuilder.DropTable(
                name: "GroupTasks");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectOwnerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_EvenTasks_GroupTaskId",
                table: "EvenTasks");

            migrationBuilder.DropColumn(
                name: "ProjectOwnerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "GroupTaskId",
                table: "EvenTasks");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "AbpUsers");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventTableId",
                table: "EvenTasks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventTables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    TableName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventTables_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvenTasks_EventTableId",
                table: "EvenTasks",
                column: "EventTableId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTables_ProjectId",
                table: "EventTables",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvenTasks_EventTables_EventTableId",
                table: "EvenTasks",
                column: "EventTableId",
                principalTable: "EventTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
