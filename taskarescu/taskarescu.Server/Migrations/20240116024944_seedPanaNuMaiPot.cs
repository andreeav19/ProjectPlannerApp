using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace taskarescu.Server.Migrations
{
    /// <inheritdoc />
<<<<<<<< HEAD:taskarescu/taskarescu.Server/Migrations/20240115214333_Init.cs
    public partial class Init : Migration
========
    public partial class seedPanaNuMaiPot : Migration
>>>>>>>> endpoints-project:taskarescu/taskarescu.Server/Migrations/20240116024944_seedPanaNuMaiPot.cs
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserBadges",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BadgeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBadges", x => new { x.UserId, x.BadgeId });
                    table.ForeignKey(
                        name: "FK_UserBadges_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBadges_Badges_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentProjects",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProjects", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_StudentProjects_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskItems_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskItems_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskItemId = table.Column<int>(type: "int", nullable: false),
                    DifficultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feedbacks_Difficulties_DifficultyId",
                        column: x => x.DifficultyId,
                        principalTable: "Difficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_TaskItems_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
<<<<<<<< HEAD:taskarescu/taskarescu.Server/Migrations/20240115214333_Init.cs
                    { "1181a8dd-e25e-4670-91f8-2aabcad89500", null, "Admin", "ADMIN" },
                    { "ad058a4a-e130-4b14-b920-f18664a19ac9", null, "Prof", "PROF" },
                    { "b0d93fdf-7e26-4440-8ada-f29850faae3e", null, "Student", "STUDENT" }
========
                    { "311c9a88-fe29-4b7c-a8bb-43aef2f3013c", null, "Prof", "PROF" },
                    { "6d0fea85-946f-453a-9897-863f79b652cb", null, "Student", "STUDENT" },
                    { "acbda893-a8e4-45f2-b3f9-2a0068b29f57", null, "Admin", "ADMIN" }
>>>>>>>> endpoints-project:taskarescu/taskarescu.Server/Migrations/20240116024944_seedPanaNuMaiPot.cs
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
<<<<<<<< HEAD:taskarescu/taskarescu.Server/Migrations/20240115214333_Init.cs
                values: new object[] { "f1a80a25-7672-46f4-b54f-e2a1afe9ead1", 0, "d1cc6a8e-ef1d-4349-ae99-adbe15724abc", "admin@admin.com", true, "Admin", "Admin", false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEGRnHOkCFJ2nPfiEF2aHPbHDqFgPTk94bwj/h3eOKr9hJbLZpTsaaE3jbHuRHAj00w==", null, false, "1181a8dd-e25e-4670-91f8-2aabcad89500", "888df048-c599-4177-946a-a879b0ce62d3", false, "admin" });
========
                values: new object[,]
                {
                    { "3b11ba9f-2b09-4b1a-b784-87e0040a2f56", 0, "051ccf30-7b18-4abb-ad7d-e2a2ce2bf33a", "sam.jones@student.com", true, "Sam", "Jones", false, null, "SAM.JONES@STUDENT.COM", "SAMJONES", "AQAAAAIAAYagAAAAELWfmcYrr5O5zMmIcZEuQc/tZ9O2Oyxy2qd3Q1ghmY5CavbDMNY5Afu3v3ObvB6kAg==", null, false, "6d0fea85-946f-453a-9897-863f79b652cb", "6e2ccc22-afdb-41e1-8ef7-07ee61d4e270", false, "samjones" },
                    { "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3", 0, "65e2dfa2-f709-40c3-8f90-74c229a845ed", "lisa.miller@student.com", true, "Lisa", "Miller", false, null, "LISA.MILLER@STUDENT.COM", "LISAMILLER", "AQAAAAIAAYagAAAAEDpp5K9sIcvwVCsSGIl4sTL4qb/E9qies7+Ah+HBxX4wMDmyuOdGOH0cCoteNqWdpA==", null, false, "6d0fea85-946f-453a-9897-863f79b652cb", "550af599-29c0-4a42-8946-273ab4d14a59", false, "lisamiller" },
                    { "590201ab-1c71-4d80-8da8-78be2bd3df9a", 0, "4b33c401-2b8c-4f88-8460-679c0978ae95", "alex.wong@student.com", true, "Alex", "Wong", false, null, "ALEX.WONG@STUDENT.COM", "ALEXWONG", "AQAAAAIAAYagAAAAEIu2NVpdx+4tMspFXPbmPapRXg84c5e4aJNOW8+hV+MyjOsnuh2I/St+HayIer5CHg==", null, false, "6d0fea85-946f-453a-9897-863f79b652cb", "c1f84cf0-0c4d-4a7f-b120-4c0dfe8d9006", false, "alexwong" },
                    { "b3a5b520-36c2-40dd-9c3a-6223a71f7f7f", 0, "6e1b7f8d-8c8d-41b6-9fa3-a3fe168ca3e2", "emily.jones@professor.com", true, "Emily", "Jones", false, null, "EMILY.JONES@PROFESSOR.COM", "EMILYJONES", "AQAAAAIAAYagAAAAEPG1+eJJopWcIjnVgCjFWpccxqoc7Fy0K8IQ04a2HNZSGAw5O9IretlEjUutWxxZ7Q==", null, false, "311c9a88-fe29-4b7c-a8bb-43aef2f3013c", "ab97223b-40ef-410a-84e6-5c21b9ec23a3", false, "emilyjones" },
                    { "bbfcea33-5568-4558-b6c0-9353518b9261", 0, "fc9939d9-a392-4eeb-be45-54b22143edce", "admin@admin.com", true, "Admin", "Admin", false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEEcY5SjyngP3QjfCH6oOe0dIEp658axYsFgMaBoJWNXE+nvA6gSzdykevB9SXZp/ww==", null, false, "acbda893-a8e4-45f2-b3f9-2a0068b29f57", "275ec1a0-09b6-41f0-828c-b072c0c52114", false, "admin" },
                    { "f2517e43-07ae-4c0f-8f63-e2481b47a5c7", 0, "b0c1a2d6-4fa9-4daa-af74-a83a13c22263", "daniel.white@professor.com", true, "Daniel", "White", false, null, "DANIEL.WHITE@PROFESSOR.COM", "DANIELWHITE", "AQAAAAIAAYagAAAAEOkGKViacqxMcB53cL1AwEzhnoXYGzT/G5JOwuDzR/i75j9LM8bOwpaOfGsqiXsM+w==", null, false, "311c9a88-fe29-4b7c-a8bb-43aef2f3013c", "36e35e19-6c6b-4880-b639-caca460b67aa", false, "danielwhite" }
                });

            migrationBuilder.InsertData(
                table: "Badges",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Awarded to users who excel in project planning.", "Project Planner" },
                    { 2, "Awarded to users who consistently complete tasks on time.", "Task Master" },
                    { 3, "Awarded to users who demonstrate excellent teamwork skills.", "Team Player" },
                    { 4, "Awarded to users who propose innovative solutions to project challenges.", "Innovator" },
                    { 5, "Awarded to users who effectively solve complex problems within a project.", "Problem Solver" },
                    { 6, "Awarded to users who excel in project communication and collaboration.", "Communication Pro" }
                });
>>>>>>>> endpoints-project:taskarescu/taskarescu.Server/Migrations/20240116024944_seedPanaNuMaiPot.cs

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Easy" },
                    { 2, "Moderate" },
                    { 3, "Intermediate" },
                    { 4, "Challenging" },
                    { 5, "Advanced" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "To Do" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });
<<<<<<<< HEAD:taskarescu/taskarescu.Server/Migrations/20240115214333_Init.cs
========

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), "Proiect în domeniul inteligenței artificiale și învățare automată.", "Inteligenta Artificiala", "b3a5b520-36c2-40dd-9c3a-6223a71f7f7f" },
                    { new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), "Proiect legat de optimizarea și gestionarea bazelor de date avansate.", "Baze de Date Avansate", "bbfcea33-5568-4558-b6c0-9353518b9261" },
                    { new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), "Implementarea unei rețele de calculatoare eficiente.", "Rețele de Calculatoare", "b3a5b520-36c2-40dd-9c3a-6223a71f7f7f" }
                });

            migrationBuilder.InsertData(
                table: "UserBadges",
                columns: new[] { "BadgeId", "UserId" },
                values: new object[,]
                {
                    { 3, "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { 6, "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { 1, "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { 4, "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { 1, "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { 2, "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { 5, "590201ab-1c71-4d80-8da8-78be2bd3df9a" }
                });

            migrationBuilder.InsertData(
                table: "StudentProjects",
                columns: new[] { "ProjectId", "UserId" },
                values: new object[,]
                {
                    { new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), "590201ab-1c71-4d80-8da8-78be2bd3df9a" }
                });

            migrationBuilder.InsertData(
                table: "TaskItems",
                columns: new[] { "Id", "Deadline", "Description", "Name", "ProjectId", "StatusId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 23, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5283), "Implementare funcționalitate X în cadrul proiectului Y.", "Implementare funcționalitate X", new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), 1, "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { 2, new DateTime(2024, 1, 26, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5372), "Testare modul Y în cadrul proiectului Z.", "Testare modul Y", new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), 1, "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { 3, new DateTime(2024, 1, 21, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5376), "Documentare proiect pentru prezentare finală.", "Documentare proiect", new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), 1, "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { 4, new DateTime(2024, 1, 19, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5380), "Soluționare bug-uri identificate în ultima versiune a proiectului.", "Soluționare bug-uri", new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), 1, "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { 5, new DateTime(2024, 1, 24, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5383), "Optimizare performanță în cadrul aplicației.", "Optimizare performanță", new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), 1, "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { 6, new DateTime(2024, 1, 22, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5391), "Implementare interfață utilizator pentru secțiunea X a proiectului.", "Implementare interfață utilizator", new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), 1, "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { 7, new DateTime(2024, 1, 25, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5405), "Testare integrare a modulelor proiectului principal.", "Testare integrare", new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), 1, "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { 8, new DateTime(2024, 1, 20, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5409), "Analiză cerințe pentru viitoarele iterații ale proiectului.", "Analiză cerințe", new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), 1, "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { 9, new DateTime(2024, 1, 23, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5417), "Optimizare performanță și structură în baza de date a proiectului.", "Optimizare bază de date", new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), 1, "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { 10, new DateTime(2024, 1, 21, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5420), "Generare raport de progres pentru săptămâna curentă.", "Raport progres săptămânal", new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), 1, "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { 11, new DateTime(2024, 1, 28, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5427), "Integrare servicii terțe în cadrul proiectului.", "Integrare servicii terțe", new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), 1, "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { 12, new DateTime(2024, 1, 24, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5431), "Documentare API pentru a fi folosit de dezvoltatori terți.", "Documentare API", new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), 1, "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { 13, new DateTime(2024, 1, 22, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5435), "Optimizare algoritmi utilizați în cadrul proiectului principal.", "Optimizare algoritmi", new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), 1, "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { 14, new DateTime(2024, 1, 27, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5439), "Implementare testare automată pentru modulele cheie ale proiectului.", "Implementare testare automată", new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), 1, "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { 15, new DateTime(2024, 1, 25, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5443), "Configurare servere pentru lansarea în producție a proiectului.", "Configurare servere de producție", new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), 1, "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { 16, new DateTime(2024, 1, 23, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5466), "Soluționare probleme identificate de auditul de securitate.", "Soluționare probleme de securitate", new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), 1, "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { 17, new DateTime(2024, 1, 30, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5470), "Creare instrumente de analiză pentru datele generate de proiect.", "Creare instrumente de analiză", new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), 1, "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { 18, new DateTime(2024, 1, 22, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5473), "Integrare cu o platformă externă pentru funcționalitate adițională.", "Integrare cu platformă externă", new Guid("7f297b67-4d4d-4e70-89a8-7e49b0b6b51e"), 1, "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { 19, new DateTime(2024, 1, 24, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5481), "Testare de securitate pentru identificarea vulnerabilităților.", "Testare securitate", new Guid("a6b66ec7-ae2a-4c7a-a1e7-4a0c1b4f4770"), 1, "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { 20, new DateTime(2024, 1, 23, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5493), "Refactorizare cod pentru îmbunătățirea structurii și performanței.", "Refactorizare cod", new Guid("c6511c7b-2970-46e1-b9f5-538a7c091cfe"), 1, "590201ab-1c71-4d80-8da8-78be2bd3df9a" }
                });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "Description", "DifficultyId", "Points", "TaskItemId", "UserId" },
                values: new object[,]
                {
                    { 1, "Feedback pentru implementarea funcționalității X.", 2, 8, 1, "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { 2, "Feedback pentru testarea modulului Y.", 3, 9, 2, "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { 3, "Feedback pentru documentarea proiectului.", 1, 7, 3, "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { 4, "Feedback pentru soluționarea bug-urilor.", 2, 6, 4, "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { 5, "Feedback pentru optimizarea performanței.", 3, 9, 5, "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { 6, "Feedback pentru implementarea interfeței utilizator.", 2, 8, 6, "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { 7, "Feedback pentru testarea integrării modulelor.", 1, 7, 7, "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { 8, "Feedback pentru analiza cerințelor.", 2, 8, 8, "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { 9, "Feedback pentru optimizarea bazei de date.", 3, 9, 9, "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { 10, "Feedback pentru raportul de progres săptămânal.", 1, 7, 10, "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" }
                });
>>>>>>>> endpoints-project:taskarescu/taskarescu.Server/Migrations/20240116024944_seedPanaNuMaiPot.cs

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Badges_Name",
                table: "Badges",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Difficulties_Name",
                table: "Difficulties",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_DifficultyId",
                table: "Feedbacks",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_TaskItemId",
                table: "Feedbacks",
                column: "TaskItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Name",
                table: "Statuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProjects_ProjectId",
                table: "StudentProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_ProjectId",
                table: "TaskItems",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_StatusId",
                table: "TaskItems",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_UserId",
                table: "TaskItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBadges_BadgeId",
                table: "UserBadges",
                column: "BadgeId");
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
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "StudentProjects");

            migrationBuilder.DropTable(
                name: "UserBadges");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropTable(
                name: "TaskItems");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
