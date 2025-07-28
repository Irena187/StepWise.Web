using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StepWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarTasks");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "ProfessionSkill");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowedAt",
                table: "UserCareerPaths",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 28, 14, 35, 22, 497, DateTimeKind.Utc).AddTicks(6982),
                comment: "When the user bookmarked this career path",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 28, 14, 24, 40, 582, DateTimeKind.Utc).AddTicks(7140),
                oldComment: "When the user bookmarked this career path");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9aa837cb-48a8-47b4-8e0e-74c57fa25b0f", "AQAAAAIAAYagAAAAEEPkMvXWLwFS24sCFleJRp7/Hdzd2aAJrQzbhJgVDOywM9B5WmFQqbL/ifNQaOwJzQ==", "d7931b28-c96a-4164-afff-c138ed8a3ac1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6731bd9a-a68f-4412-a576-c9c6895d51bc", "AQAAAAIAAYagAAAAEHyNyZ76meR0IvzAaMqT1ELUOsG7qM2Hg73cuYlq29lbXyKno8zoZ1FLKqXkPhUXKw==", "3cdec087-5442-4470-a782-44f84f2d3d8f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a805a90-9fea-4649-aa34-ce53a0a19786", "AQAAAAIAAYagAAAAECCQ9ZxY9XZwI2Cl4H0dsmz4wv+kfMf2jjTOFzScftwEVn/ao1n3Pe6/5uVDNKJfoA==", "61695034-2191-4293-97ed-060d9d7f13c3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78471f08-f570-4440-bec9-cec8cae3e518", "AQAAAAIAAYagAAAAEDiTVHX619tWWTH2saUQ07aebnKLGNenS3JoPHjmjSzUDEGlOY55wmVI11cROZFm4Q==", "751b4972-5450-4877-97bd-378bc4131473" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc5cc232-a41f-4f36-b44b-60188aeba142", "AQAAAAIAAYagAAAAEOXJ5db/wMrKoXHvM5XV4KWY9cZ2dux0/XjkpsoRoFZ+sT2mLtbKH1/3pqeTelR0+A==", "56d1c07d-3793-4632-8c36-1f69c21d3b1f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f940be7-54ed-4a92-b74b-35460fc293d9", "AQAAAAIAAYagAAAAEIhr4hr1AumOgqN2gIOhlTZ1dtLHVi7UtBIZK7QSGATMm5QO7ef8NMlKGmk0tFi//Q==", "37ecccb7-8076-4eea-8cf9-52047c7c06c9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowedAt",
                table: "UserCareerPaths",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 28, 14, 24, 40, 582, DateTimeKind.Utc).AddTicks(7140),
                comment: "When the user bookmarked this career path",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 28, 14, 35, 22, 497, DateTimeKind.Utc).AddTicks(6982),
                oldComment: "When the user bookmarked this career path");

            migrationBuilder.CreateTable(
                name: "CalendarTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarTasks", x => x.Id);
                },
                comment: "Calendar tasks that the user added to their calendar of events or deadlines");

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 20000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                },
                comment: "Notes that users can write to themselves to keep track of what they have completed.");

            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Industry = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "The industry of this profession"),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                },
                comment: "All of the professions");

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                },
                comment: "Skill in a profession");

            migrationBuilder.CreateTable(
                name: "ProfessionSkill",
                columns: table => new
                {
                    ProfessionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequiredSkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionSkill", x => new { x.ProfessionsId, x.RequiredSkillsId });
                    table.ForeignKey(
                        name: "FK_ProfessionSkill_Professions_ProfessionsId",
                        column: x => x.ProfessionsId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessionSkill_Skills_RequiredSkillsId",
                        column: x => x.RequiredSkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a56951d9-eebf-4aea-b5a1-c3c7ea87f352", "AQAAAAIAAYagAAAAEOFBuXciVGhivSZQJNcYyzApWLJZQnd+Nl2RTOjTmMKrUZfCEApfK4icv+XOArk1QA==", "52f06bfa-bc9c-4093-bcd2-225403809cb7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ab5f016-1ddf-4997-a208-b7e5e15f9b0f", "AQAAAAIAAYagAAAAEKWkX7nd40keTP9XmTxtebNdaNZX84jpWVvJxzXnEWPgGnVZ7Lw48nCgBPk882gCrw==", "f1968f68-25c2-4e15-9576-ab7cb920ef8f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b08ef21-0bd9-4383-85b1-f7d2e19a1cea", "AQAAAAIAAYagAAAAEAsE8X+BuRJe9e7vPWJl4bJ6AeUz0hANmSZguKQEDhFQHbPsu5ESp+JHVdN1GarnaA==", "56862a5e-6552-4ba9-aae7-af89b295f1b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f60a4b2f-3c13-41c3-bdf7-2828494eede8", "AQAAAAIAAYagAAAAECcy4Q4zl/tvufEDF7McStADjqNAvBgfdM2NBIXj93KLldY5vwmdvwv+WURyZJXq+Q==", "739e4dd2-1f23-4928-8866-b5e106811352" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "837c96fc-f8af-4e84-b4fe-779761e71e9f", "AQAAAAIAAYagAAAAEFJuHrOLtasn8Dp5c0w6k/bTfDZwe84ix8AWfN3lb/e6daADMxd5/9iSstQCVdIb3g==", "dbe1d4ee-638b-46c5-a90c-65a116a40cc7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e94dd863-4808-4f68-bb68-ea7ed40f7160", "AQAAAAIAAYagAAAAEBzVfHNKB1cJeu1XOw0xPXKgY1jCDTFMiynvxnMDSgKdj7QtHdGzVP5YfxRT0CFMWQ==", "15f184a9-bcb5-4c98-b400-6c8305a1bdb7" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionSkill_RequiredSkillsId",
                table: "ProfessionSkill",
                column: "RequiredSkillsId");
        }
    }
}
