using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StepWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowedAt",
                table: "UserCareerPaths",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 16, 15, 3, 55, 20, DateTimeKind.Utc).AddTicks(6917),
                comment: "When the user bookmarked this career path",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 13, 9, 39, 48, 815, DateTimeKind.Utc).AddTicks(8988),
                oldComment: "When the user bookmarked this career path");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 0, "88dd3aee-7f23-4927-b519-ef603d4b418c", "john.developer@example.com", true, true, null, "JOHN.DEVELOPER@EXAMPLE.COM", "JOHN.DEVELOPER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOKBaHjE2BYEF9ZFys625xcdQQg6grT983uOtUDthjuaBU5/822oJH0TIBfoGKjIKQ==", null, false, "af40aa4e-b1aa-48ec-82c6-c2244530d87c", false, "john.developer@example.com" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 0, "83f9b5b6-0c93-4bb9-bc08-161ac0131312", "sarah.datascientist@example.com", true, true, null, "SARAH.DATASCIENTIST@EXAMPLE.COM", "SARAH.DATASCIENTIST@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAk+IF1OgzyHC5WxPGYWexsKORmguo/76tQaNX4xxhFTNyCjn7MuwFbElKB+C4IGWw==", null, false, "04d961fa-876a-4b51-9045-640a8abae5bd", false, "sarah.datascientist@example.com" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 0, "13edcae9-bb98-4e82-89d8-574d56265c0c", "mike.mobile@example.com", true, true, null, "MIKE.MOBILE@EXAMPLE.COM", "MIKE.MOBILE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHj08JzCS41jYrSkGv95tPeKNQ+iZz0R5tEQ4oTyJXyduYufQI0IjteNiNkWDRgzZA==", null, false, "a9451209-1aad-4eea-9fa5-fff7efcce3f4", false, "mike.mobile@example.com" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 0, "3a072277-c664-42c9-8a37-7f29b249be56", "alex.devops@example.com", true, true, null, "ALEX.DEVOPS@EXAMPLE.COM", "ALEX.DEVOPS@EXAMPLE.COM", "AQAAAAIAAYagAAAAEEi/0acttbXDRlbpS4VA9paYNsdNFTlkrWh4Qqw6ivmlhZS3+H/eKqZ4xAesP4sTUw==", null, false, "d8770e00-f073-4f0d-b8c8-e365ea847c0c", false, "alex.devops@example.com" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 0, "c3d94f9a-21bb-41ca-9c40-00b18612d2e8", "emma.security@example.com", true, true, null, "EMMA.SECURITY@EXAMPLE.COM", "EMMA.SECURITY@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMiTnJjwRBvLUUm61yoig5E9vDAQodlDYUG3O5asCpymHdMRaTWxDymBocEF/8lm8A==", null, false, "eb3547de-e0a8-4c1b-b37c-b8db3c948d1f", false, "emma.security@example.com" },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), 0, "69a63dc3-89a0-46f6-9b2b-87ecacf8f95f", "david.designer@example.com", true, true, null, "DAVID.DESIGNER@EXAMPLE.COM", "DAVID.DESIGNER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMEZaLChyBQ8Myq6txJEwXaBOI7yFt5RGfqYLhRp60gQq2gR20jGdgNYOL9e1oa+Mg==", null, false, "6e3d6416-23f5-4224-a954-f759c2d42052", false, "david.designer@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Creators",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff") }
                });

            migrationBuilder.InsertData(
                table: "CareerPaths",
                columns: new[] { "Id", "CreatorId", "Description", "GoalProfession", "IsPublic", "Title" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("11111111-1111-1111-1111-111111111111"), "A comprehensive path to becoming a full-stack web developer, covering both frontend and backend technologies.", "Full-Stack Web Developer", true, "Full-Stack Web Developer" },
                    { new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("22222222-2222-2222-2222-222222222222"), "Learn the fundamentals of data science, including statistics, machine learning, and data visualization.", "Data Scientist", true, "Data Scientist" },
                    { new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"), new Guid("33333333-3333-3333-3333-333333333333"), "Master mobile app development for iOS and Android platforms using modern frameworks.", "Mobile App Developer", true, "Mobile App Developer" },
                    { new Guid("d4e5f6a7-b8c9-0123-defa-456789012345"), new Guid("44444444-4444-4444-4444-444444444444"), "Learn the practices and tools needed to bridge development and operations teams.", "DevOps Engineer", true, "DevOps Engineer" },
                    { new Guid("e5f6a7b8-c9d0-1234-efab-567890123456"), new Guid("55555555-5555-5555-5555-555555555555"), "Develop skills in information security, ethical hacking, and security architecture.", "Cybersecurity Specialist", true, "Cybersecurity Specialist" },
                    { new Guid("f6a7b8c9-d0e1-2345-fabc-678901234567"), new Guid("66666666-6666-6666-6666-666666666666"), "Learn user experience design principles and create intuitive, beautiful user interfaces.", "UX/UI Designer", true, "UX/UI Designer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CareerPaths",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"));

            migrationBuilder.DeleteData(
                table: "CareerPaths",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"));

            migrationBuilder.DeleteData(
                table: "CareerPaths",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"));

            migrationBuilder.DeleteData(
                table: "CareerPaths",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-0123-defa-456789012345"));

            migrationBuilder.DeleteData(
                table: "CareerPaths",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-1234-efab-567890123456"));

            migrationBuilder.DeleteData(
                table: "CareerPaths",
                keyColumn: "Id",
                keyValue: new Guid("f6a7b8c9-d0e1-2345-fabc-678901234567"));

            migrationBuilder.DeleteData(
                table: "Creators",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Creators",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Creators",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Creators",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Creators",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Creators",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowedAt",
                table: "UserCareerPaths",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 13, 9, 39, 48, 815, DateTimeKind.Utc).AddTicks(8988),
                comment: "When the user bookmarked this career path",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 16, 15, 3, 55, 20, DateTimeKind.Utc).AddTicks(6917),
                oldComment: "When the user bookmarked this career path");
        }
    }
}
