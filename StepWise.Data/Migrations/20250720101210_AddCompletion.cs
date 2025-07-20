using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StepWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCompletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowedAt",
                table: "UserCareerPaths",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 20, 10, 12, 10, 429, DateTimeKind.Utc).AddTicks(1822),
                comment: "When the user bookmarked this career path",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 16, 15, 3, 55, 20, DateTimeKind.Utc).AddTicks(6917),
                oldComment: "When the user bookmarked this career path");

            migrationBuilder.CreateTable(
                name: "UserCareerStepCompletions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CareerStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCareerStepCompletions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCareerStepCompletions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCareerStepCompletions_CareerSteps_CareerStepId",
                        column: x => x.CareerStepId,
                        principalTable: "CareerSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "276db0e0-1a0b-4ba5-ae12-eb58f138bb9f", "AQAAAAIAAYagAAAAEO8HtgsN/SGq/xWXCyS5cYvfd3mof4+Tyy4EO76AILDMFJ96FTmM8OL+tKGzUBk45A==", "a6233aab-5de3-410f-9068-49ba03561aab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50b21b57-d292-486c-9c38-722271fd7c58", "AQAAAAIAAYagAAAAEJbX7ewaEy3vr8+U5PhQbocMXMMbNlwy0h3rQtmIyVX8hkQj44Lq+NhoWdw0SAjGHw==", "d970de01-acb2-4d15-80cf-43b02f2da98f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c0a9e39-dab6-4a4a-8768-f4843dba74cc", "AQAAAAIAAYagAAAAEFzdGKHxDOo69uWek47PihfJ9K1xnnX0xgPeRjw6lmkSkGi3faJoNu4/bo5a/wuoPA==", "70479922-a14b-48be-9a3b-adcf745fa79f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b8238fe-24b3-4ab3-884b-6ae454cb88bb", "AQAAAAIAAYagAAAAEGOEPBcGmR9Wl9/aBGh6vpTiyW8Y1S//I2pxpwzCxGW8n0KtxBVrLfARAeuxpTUYRA==", "f7d45b84-d203-409d-bfd3-9e0f051485bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04d59d77-96d5-4d6e-b659-704501f6e3e9", "AQAAAAIAAYagAAAAEOKYI+rHmLiD4WlaTXDEVRPt0y0tPh6rxCBW7q+t0kUPEH0pIx12PDF09uvjnLB2pw==", "2e440148-1e77-42ea-8cb9-ecb70867a7ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b67acae1-7852-4d30-932d-21461e7801b1", "AQAAAAIAAYagAAAAEA3tILxVoUNdBBjGlzzrfC2dMOs+GTO4b/FlT+gv0POkFF9fDxNCVzrod0a/xkAW9g==", "ffeac33a-6f19-41ff-b2f3-203843afa253" });

            migrationBuilder.CreateIndex(
                name: "IX_UserCareerStepCompletions_CareerStepId",
                table: "UserCareerStepCompletions",
                column: "CareerStepId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCareerStepCompletions_UserId_CareerStepId",
                table: "UserCareerStepCompletions",
                columns: new[] { "UserId", "CareerStepId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCareerStepCompletions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowedAt",
                table: "UserCareerPaths",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 16, 15, 3, 55, 20, DateTimeKind.Utc).AddTicks(6917),
                comment: "When the user bookmarked this career path",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 20, 10, 12, 10, 429, DateTimeKind.Utc).AddTicks(1822),
                oldComment: "When the user bookmarked this career path");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88dd3aee-7f23-4927-b519-ef603d4b418c", "AQAAAAIAAYagAAAAEOKBaHjE2BYEF9ZFys625xcdQQg6grT983uOtUDthjuaBU5/822oJH0TIBfoGKjIKQ==", "af40aa4e-b1aa-48ec-82c6-c2244530d87c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83f9b5b6-0c93-4bb9-bc08-161ac0131312", "AQAAAAIAAYagAAAAEAk+IF1OgzyHC5WxPGYWexsKORmguo/76tQaNX4xxhFTNyCjn7MuwFbElKB+C4IGWw==", "04d961fa-876a-4b51-9045-640a8abae5bd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13edcae9-bb98-4e82-89d8-574d56265c0c", "AQAAAAIAAYagAAAAEHj08JzCS41jYrSkGv95tPeKNQ+iZz0R5tEQ4oTyJXyduYufQI0IjteNiNkWDRgzZA==", "a9451209-1aad-4eea-9fa5-fff7efcce3f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a072277-c664-42c9-8a37-7f29b249be56", "AQAAAAIAAYagAAAAEEi/0acttbXDRlbpS4VA9paYNsdNFTlkrWh4Qqw6ivmlhZS3+H/eKqZ4xAesP4sTUw==", "d8770e00-f073-4f0d-b8c8-e365ea847c0c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3d94f9a-21bb-41ca-9c40-00b18612d2e8", "AQAAAAIAAYagAAAAEMiTnJjwRBvLUUm61yoig5E9vDAQodlDYUG3O5asCpymHdMRaTWxDymBocEF/8lm8A==", "eb3547de-e0a8-4c1b-b37c-b8db3c948d1f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69a63dc3-89a0-46f6-9b2b-87ecacf8f95f", "AQAAAAIAAYagAAAAEMEZaLChyBQ8Myq6txJEwXaBOI7yFt5RGfqYLhRp60gQq2gR20jGdgNYOL9e1oa+Mg==", "6e3d6416-23f5-4224-a954-f759c2d42052" });
        }
    }
}
