using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StepWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCareerSteps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2025, 7, 24, 12, 23, 9, 312, DateTimeKind.Utc).AddTicks(1813),
                oldComment: "When the user bookmarked this career path");

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

            migrationBuilder.InsertData(
                table: "CareerSteps",
                columns: new[] { "Id", "CareerPathId", "Deadline", "Description", "Title", "Type", "Url" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), null, "Master the building blocks of web development with HTML structure and CSS styling.", "Learn HTML & CSS Fundamentals", 0, "https://www.freecodecamp.org/learn/responsive-web-design/" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), null, "Learn JavaScript programming language basics including variables, functions, and DOM manipulation.", "JavaScript Fundamentals", 0, "https://javascript.info/" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), null, "Build modern user interfaces with React components, state management, and hooks.", "React.js Framework", 0, "https://reactjs.org/tutorial/tutorial.html" },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), null, "Create server-side applications using Node.js and Express framework.", "Node.js & Express Backend", 0, null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), null, "Learn relational database design and SQL queries for data management.", "Database Design & SQL", 0, null },
                    { new Guid("10000000-0000-0000-0000-000000000006"), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), null, "Create 3-5 full-stack projects to showcase your skills to potential employers.", "Build Portfolio Projects", 7, null },
                    { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), null, "Learn Python syntax, data structures, and basic programming concepts.", "Python Programming Basics", 0, "https://www.python.org/about/gettingstarted/" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), null, "Understand statistical concepts, probability, and linear algebra fundamentals.", "Statistics and Mathematics", 1, null },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), null, "Master data manipulation and analysis using Python's most popular libraries.", "Pandas & NumPy for Data Analysis", 0, null },
                    { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), null, "Learn supervised and unsupervised learning algorithms and their applications.", "Machine Learning Fundamentals", 0, null },
                    { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), null, "Create compelling visualizations to communicate data insights effectively.", "Data Visualization with Matplotlib & Seaborn", 0, null },
                    { new Guid("30000000-0000-0000-0000-000000000001"), new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"), null, "Learn Swift fundamentals for iOS development including syntax and concepts.", "Swift Programming Language", 0, "https://developer.apple.com/swift/" },
                    { new Guid("30000000-0000-0000-0000-000000000002"), new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"), null, "Build native iOS applications using UIKit framework and Xcode.", "iOS Development with UIKit", 0, null },
                    { new Guid("30000000-0000-0000-0000-000000000003"), new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"), null, "Create Android applications using Kotlin and Android Studio.", "Android Development with Kotlin", 0, null },
                    { new Guid("30000000-0000-0000-0000-000000000004"), new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"), null, "Build apps for both iOS and Android using React Native framework.", "Cross-Platform Development with React Native", 0, null },
                    { new Guid("30000000-0000-0000-0000-000000000005"), new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"), null, "Learn the process of publishing apps to Apple App Store and Google Play Store.", "App Store Deployment", 6, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "CareerSteps",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowedAt",
                table: "UserCareerPaths",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 24, 12, 23, 9, 312, DateTimeKind.Utc).AddTicks(1813),
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
                values: new object[] { "91225b5b-bf65-4dd7-8b21-e5b10eafedfd", "AQAAAAIAAYagAAAAEDlVM720tvNMpRmeP7GcF5IA4ZckMXg7pGaeNBS9sVgs31ZTL6WCY25qrWu6d2uUKQ==", "25160196-d9e5-41b3-b0d3-81a7c6a41bf0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "029bc63b-bf52-414b-a3a9-c7319af5fd77", "AQAAAAIAAYagAAAAECmDqm8ms588rX/1m73P70n1V2jp6ehioOub7ZUB//dH2yyr/OG3W5EjbLXlwtF34g==", "69d1940b-853a-4e95-bd01-940599a1ad74" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f51dcf3-44c1-4f51-89ef-6f5e6565cb60", "AQAAAAIAAYagAAAAEMlE1bXvJU1pCba1gEQj/uwpJ3fmbIEQdhm27xQNbGSSB46IyHSQp+Kro9cMI1fFEg==", "bdae71dd-ef8b-4bda-970a-c4637dd41597" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a155838-4fa7-462d-8ec1-eb1740f0df9d", "AQAAAAIAAYagAAAAEJIWcUbVTrmkYD/PUZ/NthUkGI/1Li9jQVUtXLQ+kwAMjFutZ6zlyTgPjjEDmL4uXA==", "bcd5b326-7f22-4fd5-95a8-748f597eb995" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f5f34e2-ffa7-4844-8069-51078022caac", "AQAAAAIAAYagAAAAEFYMo2MEcCRvX486RBW8kvuNH+kVaO8sBSSptzoHWDs13T30nc+bQ35XpRElUisx+Q==", "7432203c-7bc4-4aa5-8de0-4fe4af11ddf2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad3d6d0e-c8bf-4a46-9aac-99ae4833ddfc", "AQAAAAIAAYagAAAAEFl2Ts9g8paQbRt9DAZft8B60F9vR0nmAFaORA/G7RwL9yTBaofe82YUD+q07X3RKQ==", "7f77cb02-8716-40d2-9329-8691e06619f4" });
        }
    }
}
