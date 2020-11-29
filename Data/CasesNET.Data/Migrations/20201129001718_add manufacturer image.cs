﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace CasesNET.Data.Migrations
{
    public partial class addmanufacturerimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Manufacturers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManufacturerId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_ImageUrl",
                table: "Manufacturers",
                column: "ImageUrl",
                unique: true,
                filter: "[ImageUrl] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ManufacturerId",
                table: "Images",
                column: "ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Manufacturers_ManufacturerId",
                table: "Images",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacturers_Images_ImageUrl",
                table: "Manufacturers",
                column: "ImageUrl",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Manufacturers_ManufacturerId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Manufacturers_Images_ImageUrl",
                table: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_ImageUrl",
                table: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Images_ManufacturerId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "Images");
        }
    }
}
