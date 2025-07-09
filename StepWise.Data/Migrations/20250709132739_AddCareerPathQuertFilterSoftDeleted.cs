using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StepWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCareerPathQuertFilterSoftDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CareerPaths",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-5678-90ab-cdef-123456789012"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENIq/bw9FojTYjWiEwpEqXOyLKO3Gyhi2FjyNDEgEbOtIFvPoZ8sNxzzm30nDqpaTw==");

            migrationBuilder.UpdateData(
                table: "CareerPaths",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555555"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "CareerPaths",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666666"),
                column: "IsDeleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CareerPaths");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-5678-90ab-cdef-123456789012"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAQfvoMaY9jycVvJ2JjClqgs8W4dNUHAH7kE6fkNyHK5MgYzXtZ18Vr5nYPXZpK4kw==");
        }
    }
}
