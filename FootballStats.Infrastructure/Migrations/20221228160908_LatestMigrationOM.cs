using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballStats.Infrastructure.Migrations
{
    public partial class LatestMigrationOM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trainings_CoachId",
                table: "Trainings");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_CoachId",
                table: "Trainings",
                column: "CoachId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trainings_CoachId",
                table: "Trainings");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_CoachId",
                table: "Trainings",
                column: "CoachId",
                unique: true);
        }
    }
}
