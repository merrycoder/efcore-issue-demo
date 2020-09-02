using Microsoft.EntityFrameworkCore.Migrations;

namespace EFGetStarted.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PostCommuteTimes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TravelTimeCycling = table.Column<int>(nullable: true),
                    TravelTimeDriving = table.Column<int>(nullable: true),
                    TravelTimePublicTransport = table.Column<int>(nullable: true),
                    PropertyID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCommuteTimes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostCommuteTimes_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostCommuteTimes_PropertyID",
                table: "PostCommuteTimes",
                column: "PropertyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostCommuteTimes");

            migrationBuilder.DropTable(
                name: "Properties");
        }
    }
}
