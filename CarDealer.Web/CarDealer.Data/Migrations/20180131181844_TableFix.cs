namespace CarDealer.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;

    public partial class TableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartCars_Parts_CarId",
                table: "PartCars");

            migrationBuilder.AddForeignKey(
                name: "FK_PartCars_Parts_PartId",
                table: "PartCars",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartCars_Parts_PartId",
                table: "PartCars");

            migrationBuilder.AddForeignKey(
                name: "FK_PartCars_Parts_CarId",
                table: "PartCars",
                column: "CarId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
