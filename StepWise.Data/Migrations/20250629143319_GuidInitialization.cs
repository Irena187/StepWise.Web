using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StepWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class GuidInitialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-5678-90ab-cdef-123456789012"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAECm60h6Ggowpdsl79HgV4nlHMih/oDhtE57610OYphaPsNMor5cH+KSsBpfCUmQhQQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-5678-90ab-cdef-123456789012"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENqQqff0eWGcuUuzRv5Q6oKd7tE731QlfBXv93UK8ea67SH5mi/GWmYufKqm8OJm6w==");
        }
    }
}
