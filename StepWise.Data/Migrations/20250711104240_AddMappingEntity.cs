using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StepWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMappingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareerPaths_AspNetUsers_UserId",
                table: "CareerPaths");

            migrationBuilder.DropForeignKey(
                name: "FK_CareerSteps_CareerPaths_CareerPathId",
                table: "CareerSteps");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CareerSteps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserCareerPaths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CareerPathId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowedAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "When the user bookmarked this career path"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "Is this bookmark relationship active?"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCareerPaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCareerPaths_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCareerPaths_CareerPaths_CareerPathId",
                        column: x => x.CareerPathId,
                        principalTable: "CareerPaths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-5678-90ab-cdef-123456789012"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENCngmgvsUprVyPEobEOfdzfOwS/Ei5L7AztDHzniJJ/O8iBE3qMoJIpZDaa/T7n5g==");

            migrationBuilder.UpdateData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-1111-2222-3333-444444444444"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-2222-3333-4444-555555555555"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.CreateIndex(
                name: "IX_UserCareerPathFollow_UserId_CareerPathId",
                table: "UserCareerPaths",
                columns: new[] { "UserId", "CareerPathId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCareerPaths_CareerPathId",
                table: "UserCareerPaths",
                column: "CareerPathId");

            migrationBuilder.AddForeignKey(
                name: "FK_CareerPaths_AspNetUsers_UserId",
                table: "CareerPaths",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CareerSteps_CareerPaths_CareerPathId",
                table: "CareerSteps",
                column: "CareerPathId",
                principalTable: "CareerPaths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CareerPaths_AspNetUsers_UserId",
                table: "CareerPaths");

            migrationBuilder.DropForeignKey(
                name: "FK_CareerSteps_CareerPaths_CareerPathId",
                table: "CareerSteps");

            migrationBuilder.DropTable(
                name: "UserCareerPaths");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CareerSteps");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-5678-90ab-cdef-123456789012"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENIq/bw9FojTYjWiEwpEqXOyLKO3Gyhi2FjyNDEgEbOtIFvPoZ8sNxzzm30nDqpaTw==");

            migrationBuilder.AddForeignKey(
                name: "FK_CareerPaths_AspNetUsers_UserId",
                table: "CareerPaths",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CareerSteps_CareerPaths_CareerPathId",
                table: "CareerSteps",
                column: "CareerPathId",
                principalTable: "CareerPaths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
