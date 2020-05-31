using Microsoft.EntityFrameworkCore.Migrations;

namespace Psycho.DAL.Migrations
{
    public partial class ExtentAppointmentResultTableWithServicesJsonColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorConclusion",
                table: "AppointmentResults");

            migrationBuilder.DropColumn(
                name: "IsHealth",
                table: "AppointmentResults");

            migrationBuilder.AddColumn<string>(
                name: "ServicesJson",
                table: "AppointmentResults",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServicesJson",
                table: "AppointmentResults");

            migrationBuilder.AddColumn<string>(
                name: "DoctorConclusion",
                table: "AppointmentResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHealth",
                table: "AppointmentResults",
                type: "bit",
                nullable: true);
        }
    }
}
