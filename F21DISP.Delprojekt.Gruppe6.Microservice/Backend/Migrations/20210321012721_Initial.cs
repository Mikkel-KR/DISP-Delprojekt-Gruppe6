using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Haandvaerker",
                columns: table => new
                {
                    HaandvaerkerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HVAnsaettelsedato = table.Column<DateTime>(nullable: false),
                    HVEfternavn = table.Column<string>(nullable: true),
                    HVFagomraade = table.Column<string>(nullable: true),
                    HVFornavn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Haandvaerker", x => x.HaandvaerkerId);
                });

            migrationBuilder.CreateTable(
                name: "Vaerktoejskasse",
                columns: table => new
                {
                    VTKId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VTKAnskaffet = table.Column<DateTime>(nullable: false),
                    VTKFabrikat = table.Column<string>(nullable: true),
                    VTKEjesAf = table.Column<int>(nullable: true),
                    VTKModel = table.Column<string>(nullable: true),
                    VTKSerienummer = table.Column<string>(nullable: true),
                    VTKFarve = table.Column<string>(nullable: true),
                    EjesAfNavigationHaandvaerkerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaerktoejskasse", x => x.VTKId);
                    table.ForeignKey(
                        name: "FK_Vaerktoejskasse_Haandvaerker_EjesAfNavigationHaandvaerkerId",
                        column: x => x.EjesAfNavigationHaandvaerkerId,
                        principalTable: "Haandvaerker",
                        principalColumn: "HaandvaerkerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vaerktoej",
                columns: table => new
                {
                    VTId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VTAnskaffet = table.Column<DateTime>(nullable: false),
                    VTFabrikat = table.Column<string>(nullable: true),
                    VTModel = table.Column<string>(nullable: true),
                    VTSerienr = table.Column<string>(nullable: true),
                    VTType = table.Column<string>(nullable: true),
                    LiggerIvtk = table.Column<int>(nullable: true),
                    LiggerIvtkNavigationVTKId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaerktoej", x => x.VTId);
                    table.ForeignKey(
                        name: "FK_Vaerktoej_Vaerktoejskasse_LiggerIvtkNavigationVTKId",
                        column: x => x.LiggerIvtkNavigationVTKId,
                        principalTable: "Vaerktoejskasse",
                        principalColumn: "VTKId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vaerktoej_LiggerIvtkNavigationVTKId",
                table: "Vaerktoej",
                column: "LiggerIvtkNavigationVTKId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaerktoejskasse_EjesAfNavigationHaandvaerkerId",
                table: "Vaerktoejskasse",
                column: "EjesAfNavigationHaandvaerkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vaerktoej");

            migrationBuilder.DropTable(
                name: "Vaerktoejskasse");

            migrationBuilder.DropTable(
                name: "Haandvaerker");
        }
    }
}
