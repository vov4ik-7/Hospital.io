using Microsoft.EntityFrameworkCore.Migrations;

namespace Psycho.DAL.Migrations
{
    public partial class ExtendAppointmentsTableWithFinishedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Appointments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Appointments");
        }
    }
}
