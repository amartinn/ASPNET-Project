﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace CasesNET.Data.Migrations
{
    public partial class updatecasewithNameandremovecasetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseType",
                table: "Cases");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cases",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cases");

            migrationBuilder.AddColumn<int>(
                name: "CaseType",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
