using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StepWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddManagerToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Creators_UserId",
                table: "Creators");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowedAt",
                table: "UserCareerPaths",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 22, 12, 28, 16, 292, DateTimeKind.Utc).AddTicks(5361),
                comment: "When the user bookmarked this career path",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 21, 9, 29, 26, 41, DateTimeKind.Utc).AddTicks(5050),
                oldComment: "When the user bookmarked this career path");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40dd9576-7321-4894-9d9a-6caf2ff5f71f", "AQAAAAIAAYagAAAAEDoJ05rqlaSNEmvEWYfJd8aAvkIyqtm5w97mPkxO5n41Fet8OSddpXU2z0keJdF3jQ==", "33d6b8e7-acc6-48de-a784-a5d2a9f96068" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20dc7150-ef60-469a-af11-d241db1ffb2b", "AQAAAAIAAYagAAAAEKMlf5xhKDYJY8NUGNyTgnADacvoD7IaP8cEyiS9Ma0iUqttHwZ260BV6qN+7ItHyw==", "ffc57ae1-70d6-4655-8c15-28a0941d59ed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74382604-e3be-421e-8e60-4a33a433bcfd", "AQAAAAIAAYagAAAAEElVd/qyHwfu8vZDX7ppoFaAdSe31pT/CSvMFVsXG4kFpwJN3NKZqYeWqK4w2AUWaQ==", "932ae35f-74e5-4e1f-872e-5b5edbeed7a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5117350-f120-4e4d-b5a4-437c221253c8", "AQAAAAIAAYagAAAAEBUK1p+Pwu5xZyamIiuHFGdXucrah2C2SO8BFsUBUue460Ld129E0BiltPGs6y9ONg==", "b63237ab-c66d-4781-9d3f-b50448c20403" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8190bacf-9546-439c-b802-633769be661c", "AQAAAAIAAYagAAAAEBq0civ4RdM9UgnG7JO5NCykpUclyvzpTifLxvlIOjKyuqLHE+NWQlC1CU0UJJr/NA==", "c90eb0f7-7a92-41a3-92ef-e615a1aab87c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3bf78632-5ec2-4535-bffe-8dbc537095b6", "AQAAAAIAAYagAAAAEC2Oln05RfQH3hRd2oHvLYY0C+qc0aSBs8sTCNPXSCMV5UmNJ87G6OOACu2F9qgZ0Q==", "ddc7d9c5-e7a8-4748-a949-b7eb537f7967" });

            migrationBuilder.CreateIndex(
                name: "IX_Creators_UserId",
                table: "Creators",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Creators_UserId",
                table: "Creators");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowedAt",
                table: "UserCareerPaths",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 21, 9, 29, 26, 41, DateTimeKind.Utc).AddTicks(5050),
                comment: "When the user bookmarked this career path",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 22, 12, 28, 16, 292, DateTimeKind.Utc).AddTicks(5361),
                oldComment: "When the user bookmarked this career path");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20b82fac-c8a0-476d-8287-1fef77f11249", "AQAAAAIAAYagAAAAEHr8OBvWU72cxIt8g4HUwm7PeB7ufKV1K/DNlqsaHHMati0DbU4JZCsYGzudZoozug==", "31183f0a-5335-4135-89d8-685871e3dce1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2eac3b33-3146-46a7-a769-a838dbf5d16c", "AQAAAAIAAYagAAAAECCViEG+BEqcEBgNnEed22yu2gZo16nMYe0dNNqK0fAePkBrLUHcMqVevVScUCPZxA==", "f3ba8e6c-4cff-46fd-8f6d-2bca4ed59270" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b603b35d-0ace-489a-8a8f-0fe7f42ccb45", "AQAAAAIAAYagAAAAEFqdM/EFcmSeV+lGrKSSDFfPMfgaUc53pcHOXHplq/f4gRjpG/O4M8lXTh2174S4xg==", "baeb8d54-827c-48ee-b420-400c45358189" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "192082e5-638e-4649-9b9f-8ff80322d76f", "AQAAAAIAAYagAAAAENstvDyYaMJQScy3WRsvMJWGysVrDAiy+y2ypQhxa9RO3XK5Gv3UIEycSe3RgwlstQ==", "bb96449f-d1b9-4155-b7ea-fb95966a08cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dfb331de-82e6-43d9-8b34-1921a5787988", "AQAAAAIAAYagAAAAEHKLQ57qk96TovWbdr5PzYr7If/5dRv1w28f7KBDnynRuJy+lZmI250WLrVbRZQbwg==", "3477bc82-72bb-4cb3-8fcd-d57c0bef49fc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d81e7fb8-c711-4536-b4fd-f503edab1f47", "AQAAAAIAAYagAAAAEOtThMwHX+CpPblr7FVdb6/K17GJv4al06srEtpxFKgTBwDJCD9dCp5PI2jfjHOj2g==", "7aa9e046-f923-47c5-b9f4-0448630a3165" });

            migrationBuilder.CreateIndex(
                name: "IX_Creators_UserId",
                table: "Creators",
                column: "UserId");
        }
    }
}
