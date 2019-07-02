using Microsoft.EntityFrameworkCore.Migrations;

namespace PMTool16Bit.Migrations
{
    public partial class updateProjectMember_addProjectRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectRole",
                table: "ProjectMembers",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectRole",
                table: "ProjectMembers");
        }
    }
}