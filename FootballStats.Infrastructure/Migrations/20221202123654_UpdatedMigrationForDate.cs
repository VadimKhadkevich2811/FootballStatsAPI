using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballStats.Infrastructure.Migrations
{
    public partial class UpdatedMigrationForDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TrainingDate",
                table: "Trainings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainingDate",
                table: "Trainings");
        }
    }
}
