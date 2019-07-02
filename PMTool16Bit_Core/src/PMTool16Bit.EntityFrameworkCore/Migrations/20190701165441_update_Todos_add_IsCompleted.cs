using Microsoft.EntityFrameworkCore.Migrations;

namespace PMTool16Bit.Migrations
{
    public partial class update_Todos_add_IsCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Todos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Todos");
        }
    }
}