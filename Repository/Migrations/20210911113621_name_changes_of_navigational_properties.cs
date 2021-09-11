using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class name_changes_of_navigational_properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Clients_ClientId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "ClientContactInfo");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Clients",
                newName: "ClientEntityId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Cars",
                newName: "ClientEntityId");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "Cars",
                newName: "CarEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_ClientId",
                table: "Cars",
                newName: "IX_Cars_ClientEntityId");

            migrationBuilder.CreateTable(
                name: "ClientContactInfoEntity",
                columns: table => new
                {
                    ClientContactInfoEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ClientEntityId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientContactInfoEntity", x => x.ClientContactInfoEntityId);
                    table.ForeignKey(
                        name: "FK_ClientContactInfoEntity_Clients_ClientEntityId",
                        column: x => x.ClientEntityId,
                        principalTable: "Clients",
                        principalColumn: "ClientEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientContactInfoEntity_ClientEntityId",
                table: "ClientContactInfoEntity",
                column: "ClientEntityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Clients_ClientEntityId",
                table: "Cars",
                column: "ClientEntityId",
                principalTable: "Clients",
                principalColumn: "ClientEntityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Clients_ClientEntityId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "ClientContactInfoEntity");

            migrationBuilder.RenameColumn(
                name: "ClientEntityId",
                table: "Clients",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "ClientEntityId",
                table: "Cars",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "CarEntityId",
                table: "Cars",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_ClientEntityId",
                table: "Cars",
                newName: "IX_Cars_ClientId");

            migrationBuilder.CreateTable(
                name: "ClientContactInfo",
                columns: table => new
                {
                    ClientContactInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientContactInfo", x => x.ClientContactInfoId);
                    table.ForeignKey(
                        name: "FK_ClientContactInfo_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientContactInfo_ClientId",
                table: "ClientContactInfo",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Clients_ClientId",
                table: "Cars",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
