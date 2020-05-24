using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Psycho.DAL.Migrations
{
    public partial class AddAnalysesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analyses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    AppointmentId = table.Column<int>(nullable: false),
                    AnalysisResult = table.Column<string>(nullable: true),
                    DoctorConclusion = table.Column<string>(nullable: true),
                    File = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analyses");
        }
    }
}
