using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWithBlazor.Migrations
{
    public partial class AddDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Departament",
                table: "ResponsabilModel",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Departament",
                table: "ResponsabilModel");
        }
    }
}
