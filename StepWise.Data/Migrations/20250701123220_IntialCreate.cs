using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StepWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareerPaths_AspNetUsers_UserId",
                table: "CareerPaths");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CareerPaths",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-5678-90ab-cdef-123456789012"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAECoEFvsHj/Sg7lY8UEK4WE0d4Xsn9+D6y1G1yeL4lira+MZ0ex2PQxLhxE5PmEfiSQ==");

            migrationBuilder.AddForeignKey(
                name: "FK_CareerPaths_AspNetUsers_UserId",
                table: "CareerPaths",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareerPaths_AspNetUsers_UserId",
                table: "CareerPaths");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CareerPaths",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-5678-90ab-cdef-123456789012"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOtVtiE46V1nVNOzKpNJamZriqAJsjMVDfDD5HH4+dk5fSUrEzroc7KYk/stkzyLhQ==");

            migrationBuilder.AddForeignKey(
                name: "FK_CareerPaths_AspNetUsers_UserId",
                table: "CareerPaths",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
