using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class modification_in_car_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cars_VIN",
                table: "Cars",
                column: "VIN",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cars_VIN",
                table: "Cars");
        }
    }
}
