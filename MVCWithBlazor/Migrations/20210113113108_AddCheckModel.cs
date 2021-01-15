using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWithBlazor.Migrations
{
    public partial class AddCheckModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "ActiuneModels",
                columns: table => new
                {
                    ActiuneModelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descriere = table.Column<string>(maxLength: 500, nullable: false),
                    Tip = table.Column<int>(nullable: false),
                    IsUseInputText = table.Column<bool>(nullable: false),
                    InputText = table.Column<string>(maxLength: 250, nullable: true),
                    UtilajModelID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiuneModels", x => x.ActiuneModelID);
                    table.ForeignKey(
                        name: "FK_ActiuneModels_UtilajModels_UtilajModelID",
                        column: x => x.UtilajModelID,
                        principalTable: "UtilajModels",
                        principalColumn: "UtilajModelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckModels",
                columns: table => new
                {
                    ActionCheckModelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataLucru = table.Column<DateTime>(nullable: false),
                    IsCheckedByOp = table.Column<bool>(nullable: false),
                    InputText = table.Column<string>(maxLength: 5, nullable: true),
                    ActiuneModelID = table.Column<int>(nullable: true),
                    DataBifatByOp = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(maxLength: 100, nullable: true),
                    IsCheckedBySef = table.Column<bool>(nullable: false),
                    SefDeSchimb = table.Column<string>(maxLength: 100, nullable: true),
                    DataBifatBySef = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckModels", x => x.ActionCheckModelID);
                    table.ForeignKey(
                        name: "FK_CheckModels_ActiuneModels_ActiuneModelID",
                        column: x => x.ActiuneModelID,
                        principalTable: "ActiuneModels",
                        principalColumn: "ActiuneModelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiuneModels_UtilajModelID",
                table: "ActiuneModels",
                column: "UtilajModelID");

            migrationBuilder.CreateIndex(
                name: "IX_CheckModels_ActiuneModelID",
                table: "CheckModels",
                column: "ActiuneModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckModels");

            migrationBuilder.DropTable(
                name: "ActiuneModels");

        }
    }
}
