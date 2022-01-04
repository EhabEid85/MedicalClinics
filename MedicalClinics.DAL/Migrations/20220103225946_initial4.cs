using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalClinics.DAL.Migrations
{
    public partial class initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CanceledReason",
                table: "Appointments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Appointments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanceledReason",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Appointments");
        }
    }
}
