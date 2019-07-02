using Microsoft.EntityFrameworkCore.Migrations;

namespace PMTool16Bit.Migrations
{
    public partial class updateTaskGroup_addShortDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "TaskGroups",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "TaskGroups");
        }
    }
}