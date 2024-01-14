using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace taskarescu.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedStatusDifficulty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e5e2470-e106-40c6-9a7a-526c45f88956");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84c3cf6a-4793-45c3-aaf0-c328e5daa52a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98cfacad-ac9d-4b63-bf84-aa2f648fa66c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d859d5f-9ee2-4e44-b6dd-5457d0c65d1c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a152702-7658-43a1-b046-d2e9200a60ef", null, "Prof", "PROF" },
                    { "4357d4d8-71f2-4fa4-960e-a2d5fd41b09d", null, "Admin", "ADMIN" },
                    { "e2ba352a-0865-4a00-bd41-eed99f73d425", null, "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bbfcea33-5568-4558-b6c0-9353518b9261", 0, "65efd9bc-8bf5-43ec-8cdb-ab9bf48bd964", "admin@admin.com", true, "Admin", "Admin", false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEJHWEaTzrjgYlM5wo7VfZIDprkwkgmj/d6lvXbSNdKXkfXLGtR0M/cjaBK49tosmjg==", null, false, "4357d4d8-71f2-4fa4-960e-a2d5fd41b09d", "a63f1f15-d95c-4437-a72e-73aa9478616d", false, "admin" });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a152702-7658-43a1-b046-d2e9200a60ef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4357d4d8-71f2-4fa4-960e-a2d5fd41b09d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2ba352a-0865-4a00-bd41-eed99f73d425");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbfcea33-5568-4558-b6c0-9353518b9261");

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6e5e2470-e106-40c6-9a7a-526c45f88956", null, "Prof", "PROF" },
                    { "84c3cf6a-4793-45c3-aaf0-c328e5daa52a", null, "Admin", "ADMIN" },
                    { "98cfacad-ac9d-4b63-bf84-aa2f648fa66c", null, "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3d859d5f-9ee2-4e44-b6dd-5457d0c65d1c", 0, "585ce446-3b61-4565-85d0-7759256afccc", "admin@admin.com", true, "Admin", "Admin", false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEIfdO8VlBgyEn16W7fAMKmFYXLsvp44ROZYv0ZsCS4+5Vit/bRF6K9eIMNkRs0TObw==", null, false, "84c3cf6a-4793-45c3-aaf0-c328e5daa52a", "75a8ad45-49ef-4bf0-89f8-32a03dcc2d97", false, "admin" });
        }
    }
}
