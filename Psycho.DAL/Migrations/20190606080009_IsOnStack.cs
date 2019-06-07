using Microsoft.EntityFrameworkCore.Migrations;

namespace Psycho.DAL.Migrations
{
    public partial class IsOnStack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isOnStack",
                table: "Reports",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isOnStack",
                table: "Reports");
        }
    }
}
