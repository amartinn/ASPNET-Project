using Microsoft.EntityFrameworkCore.Migrations;

namespace CasesNET.Data.Migrations
{
    public partial class casetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaseType",
                table: "Cases",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseType",
                table: "Cases");
        }
    }
}
