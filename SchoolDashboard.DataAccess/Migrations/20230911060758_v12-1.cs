using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolDashboard.DataAccess.Migrations
{
    public partial class v121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LectureLecturerName",
                table: "Lectures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LectureLecturerName",
                table: "Lectures");
        }
    }
}
