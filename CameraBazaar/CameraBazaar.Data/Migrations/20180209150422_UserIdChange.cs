using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CameraBazaar.Data.Migrations
{
    public partial class UserIdChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cameras_AspNetUsers_UserId1",
                table: "Cameras");

            migrationBuilder.DropIndex(
                name: "IX_Cameras_UserId1",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Cameras");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Cameras",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_UserId",
                table: "Cameras",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cameras_AspNetUsers_UserId",
                table: "Cameras",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cameras_AspNetUsers_UserId",
                table: "Cameras");

            migrationBuilder.DropIndex(
                name: "IX_Cameras_UserId",
                table: "Cameras");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Cameras",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Cameras",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_UserId1",
                table: "Cameras",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cameras_AspNetUsers_UserId1",
                table: "Cameras",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
