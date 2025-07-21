using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StepWise.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "The industry of this profession"),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalendarTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarTasks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Calendar tasks that the user added to their calendar of events or deadlines");

            migrationBuilder.CreateTable(
                name: "Creators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Creators_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 20000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Notes that users can write to themselves to keep track of what they have completed.");

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

            migrationBuilder.CreateTable(
                name: "CareerPaths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Did the user make this career path public or private?"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoalProfession = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "The final profession that this career path leads to."),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerPaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CareerPaths_Creators_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Creators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CareerSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "The type of step(Course, Book, Job...)"),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true, comment: "The url address to the reference."),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Did the user complete this step?"),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "When is the time this step should be completed?"),
                    CareerPathId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CareerSteps_CareerPaths_CareerPathId",
                        column: x => x.CareerPathId,
                        principalTable: "CareerPaths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCareerPaths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CareerPathId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 7, 21, 9, 29, 26, 41, DateTimeKind.Utc).AddTicks(5050), comment: "When the user bookmarked this career path"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Is this bookmark relationship active?"),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCareerPaths_CareerPaths_CareerPathId",
                        column: x => x.CareerPathId,
                        principalTable: "CareerPaths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 0, "20b82fac-c8a0-476d-8287-1fef77f11249", "john.developer@example.com", true, true, null, "JOHN.DEVELOPER@EXAMPLE.COM", "JOHN.DEVELOPER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHr8OBvWU72cxIt8g4HUwm7PeB7ufKV1K/DNlqsaHHMati0DbU4JZCsYGzudZoozug==", null, false, "31183f0a-5335-4135-89d8-685871e3dce1", false, "john.developer@example.com" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 0, "2eac3b33-3146-46a7-a769-a838dbf5d16c", "sarah.datascientist@example.com", true, true, null, "SARAH.DATASCIENTIST@EXAMPLE.COM", "SARAH.DATASCIENTIST@EXAMPLE.COM", "AQAAAAIAAYagAAAAECCViEG+BEqcEBgNnEed22yu2gZo16nMYe0dNNqK0fAePkBrLUHcMqVevVScUCPZxA==", null, false, "f3ba8e6c-4cff-46fd-8f6d-2bca4ed59270", false, "sarah.datascientist@example.com" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 0, "b603b35d-0ace-489a-8a8f-0fe7f42ccb45", "mike.mobile@example.com", true, true, null, "MIKE.MOBILE@EXAMPLE.COM", "MIKE.MOBILE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEFqdM/EFcmSeV+lGrKSSDFfPMfgaUc53pcHOXHplq/f4gRjpG/O4M8lXTh2174S4xg==", null, false, "baeb8d54-827c-48ee-b420-400c45358189", false, "mike.mobile@example.com" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 0, "192082e5-638e-4649-9b9f-8ff80322d76f", "alex.devops@example.com", true, true, null, "ALEX.DEVOPS@EXAMPLE.COM", "ALEX.DEVOPS@EXAMPLE.COM", "AQAAAAIAAYagAAAAENstvDyYaMJQScy3WRsvMJWGysVrDAiy+y2ypQhxa9RO3XK5Gv3UIEycSe3RgwlstQ==", null, false, "bb96449f-d1b9-4155-b7ea-fb95966a08cc", false, "alex.devops@example.com" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 0, "dfb331de-82e6-43d9-8b34-1921a5787988", "emma.security@example.com", true, true, null, "EMMA.SECURITY@EXAMPLE.COM", "EMMA.SECURITY@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHKLQ57qk96TovWbdr5PzYr7If/5dRv1w28f7KBDnynRuJy+lZmI250WLrVbRZQbwg==", null, false, "3477bc82-72bb-4cb3-8fcd-d57c0bef49fc", false, "emma.security@example.com" },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), 0, "d81e7fb8-c711-4536-b4fd-f503edab1f47", "david.designer@example.com", true, true, null, "DAVID.DESIGNER@EXAMPLE.COM", "DAVID.DESIGNER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOtThMwHX+CpPblr7FVdb6/K17GJv4al06srEtpxFKgTBwDJCD9dCp5PI2jfjHOj2g==", null, false, "7aa9e046-f923-47c5-b9f4-0448630a3165", false, "david.designer@example.com" }
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarTasks_UserId",
                table: "CalendarTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CareerPaths_CreatorId",
                table: "CareerPaths",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CareerPaths_GoalProfession",
                table: "CareerPaths",
                column: "GoalProfession");

            migrationBuilder.CreateIndex(
                name: "IX_CareerPaths_IsDeleted",
                table: "CareerPaths",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CareerPaths_IsPublic",
                table: "CareerPaths",
                column: "IsPublic");

            migrationBuilder.CreateIndex(
                name: "IX_CareerSteps_CareerPathId",
                table: "CareerSteps",
                column: "CareerPathId");

            migrationBuilder.CreateIndex(
                name: "IX_CareerSteps_Deadline",
                table: "CareerSteps",
                column: "Deadline");

            migrationBuilder.CreateIndex(
                name: "IX_CareerSteps_IsCompleted",
                table: "CareerSteps",
                column: "IsCompleted");

            migrationBuilder.CreateIndex(
                name: "IX_CareerSteps_IsDeleted",
                table: "CareerSteps",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CareerSteps_Type",
                table: "CareerSteps",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Creators_IsDeleted",
                table: "Creators",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Creators_UserId",
                table: "Creators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionSkill_RequiredSkillsId",
                table: "ProfessionSkill",
                column: "RequiredSkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCareerPaths_CareerPathId",
                table: "UserCareerPaths",
                column: "CareerPathId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCareerPaths_FollowedAt",
                table: "UserCareerPaths",
                column: "FollowedAt");

            migrationBuilder.CreateIndex(
                name: "IX_UserCareerPaths_IsActive",
                table: "UserCareerPaths",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_UserCareerPaths_IsDeleted",
                table: "UserCareerPaths",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserCareerPaths_UserId",
                table: "UserCareerPaths",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCareerPaths_UserId_CareerPathId",
                table: "UserCareerPaths",
                columns: new[] { "UserId", "CareerPathId" },
                unique: true);

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CalendarTasks");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "ProfessionSkill");

            migrationBuilder.DropTable(
                name: "UserCareerPaths");

            migrationBuilder.DropTable(
                name: "UserCareerStepCompletions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "CareerSteps");

            migrationBuilder.DropTable(
                name: "CareerPaths");

            migrationBuilder.DropTable(
                name: "Creators");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
