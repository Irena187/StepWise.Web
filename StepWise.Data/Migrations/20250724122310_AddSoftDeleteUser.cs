using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StepWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarTasks_AspNetUsers_UserId",
                table: "CalendarTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_AspNetUsers_UserId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_UserId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_CalendarTasks_UserId",
                table: "CalendarTasks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CalendarTasks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowedAt",
                table: "UserCareerPaths",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 24, 12, 23, 9, 312, DateTimeKind.Utc).AddTicks(1813),
                comment: "When the user bookmarked this career path",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 22, 12, 28, 16, 292, DateTimeKind.Utc).AddTicks(5361),
                oldComment: "When the user bookmarked this career path");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91225b5b-bf65-4dd7-8b21-e5b10eafedfd", false, "AQAAAAIAAYagAAAAEDlVM720tvNMpRmeP7GcF5IA4ZckMXg7pGaeNBS9sVgs31ZTL6WCY25qrWu6d2uUKQ==", "25160196-d9e5-41b3-b0d3-81a7c6a41bf0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "029bc63b-bf52-414b-a3a9-c7319af5fd77", false, "AQAAAAIAAYagAAAAECmDqm8ms588rX/1m73P70n1V2jp6ehioOub7ZUB//dH2yyr/OG3W5EjbLXlwtF34g==", "69d1940b-853a-4e95-bd01-940599a1ad74" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f51dcf3-44c1-4f51-89ef-6f5e6565cb60", false, "AQAAAAIAAYagAAAAEMlE1bXvJU1pCba1gEQj/uwpJ3fmbIEQdhm27xQNbGSSB46IyHSQp+Kro9cMI1fFEg==", "bdae71dd-ef8b-4bda-970a-c4637dd41597" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a155838-4fa7-462d-8ec1-eb1740f0df9d", false, "AQAAAAIAAYagAAAAEJIWcUbVTrmkYD/PUZ/NthUkGI/1Li9jQVUtXLQ+kwAMjFutZ6zlyTgPjjEDmL4uXA==", "bcd5b326-7f22-4fd5-95a8-748f597eb995" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f5f34e2-ffa7-4844-8069-51078022caac", false, "AQAAAAIAAYagAAAAEFYMo2MEcCRvX486RBW8kvuNH+kVaO8sBSSptzoHWDs13T30nc+bQ35XpRElUisx+Q==", "7432203c-7bc4-4aa5-8de0-4fe4af11ddf2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad3d6d0e-c8bf-4a46-9aac-99ae4833ddfc", false, "AQAAAAIAAYagAAAAEFl2Ts9g8paQbRt9DAZft8B60F9vR0nmAFaORA/G7RwL9yTBaofe82YUD+q07X3RKQ==", "7f77cb02-8716-40d2-9329-8691e06619f4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowedAt",
                table: "UserCareerPaths",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 22, 12, 28, 16, 292, DateTimeKind.Utc).AddTicks(5361),
                comment: "When the user bookmarked this career path",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 24, 12, 23, 9, 312, DateTimeKind.Utc).AddTicks(1813),
                oldComment: "When the user bookmarked this career path");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CalendarTasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarTasks_UserId",
                table: "CalendarTasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarTasks_AspNetUsers_UserId",
                table: "CalendarTasks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_AspNetUsers_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
