using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace taskarescu.Server.Migrations
{
    /// <inheritdoc />
    public partial class seedFioros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6d0fea85-946f-453a-9897-863f79b652cb", "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" },
                    { "6d0fea85-946f-453a-9897-863f79b652cb", "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" },
                    { "6d0fea85-946f-453a-9897-863f79b652cb", "590201ab-1c71-4d80-8da8-78be2bd3df9a" },
                    { "311c9a88-fe29-4b7c-a8bb-43aef2f3013c", "b3a5b520-36c2-40dd-9c3a-6223a71f7f7f" },
                    { "acbda893-a8e4-45f2-b3f9-2a0068b29f57", "bbfcea33-5568-4558-b6c0-9353518b9261" },
                    { "311c9a88-fe29-4b7c-a8bb-43aef2f3013c", "f2517e43-07ae-4c0f-8f63-e2481b47a5c7" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b11ba9f-2b09-4b1a-b784-87e0040a2f56",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29c55338-5c82-47f5-8787-c3f92f62e0c0", "AQAAAAIAAYagAAAAEGvjc/gOX7OHskYeNQa92mZJcspJZChCvM7qrxPRZzK7ScGAM2HefV0n+UZGE1ePXw==", "4d185be1-3894-4920-b2cf-8bff77035ce1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "03de9f9c-c22e-4f49-94cb-0fb8046c0aa4", "AQAAAAIAAYagAAAAECm6G+4ddXfOzVVrzwX/O/KiNHj4N/RYjzEXtDOEwPAW4JC6mx0fsocy60SN22LmZQ==", "fc493445-f32d-45fc-92f1-5fd937ee3287" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "590201ab-1c71-4d80-8da8-78be2bd3df9a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96c87d41-b595-436c-b59d-8d4958c1d66e", "AQAAAAIAAYagAAAAEE3QQDvIuHXp+L+LMROH08cBettEpxdaiOLeeFXA9uJ8+3OpHhpBz3XCpJbMYJ/U/Q==", "fad2118d-5e12-43da-a91f-c3fef11dc4e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3a5b520-36c2-40dd-9c3a-6223a71f7f7f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0bac1479-735b-4ebc-894a-0cb9dcf6cf32", "AQAAAAIAAYagAAAAEOsQ36zg5Tl7RuyL9r4MS7nhwmDRXA/UekZZR722DAoh0o6sOGLoTQ5kIB9rEgbeDg==", "52689cdd-7078-4125-b80d-6b1d486d9821" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbfcea33-5568-4558-b6c0-9353518b9261",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cccc149e-e85d-4fb2-bd1c-fc7a08b09180", "AQAAAAIAAYagAAAAEGvexbT/YT4A56i3ECXGNfE5vktMy373tIo5TSfno3yqrSnqAQM7f3Ja49K0XCxuQg==", "d4379adc-d186-4f02-a0ac-2e867e8ece68" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f2517e43-07ae-4c0f-8f63-e2481b47a5c7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d83a5500-445c-40c3-94df-c634b5f8ab45", "AQAAAAIAAYagAAAAEEuUzuCzUAQ5pwdOys+TmICewuvLSawUnMm7M2Cq2UXvDf+jYun97Cg0svHAWvnNbQ==", "416d6b56-30ff-45af-86c8-55acafa7a066" });

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deadline",
                value: new DateTime(2024, 1, 23, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(5867));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deadline",
                value: new DateTime(2024, 1, 26, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(5977));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deadline",
                value: new DateTime(2024, 1, 21, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(5986));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "Deadline",
                value: new DateTime(2024, 1, 19, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(5994));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "Deadline",
                value: new DateTime(2024, 1, 24, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6001));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "Deadline",
                value: new DateTime(2024, 1, 22, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6006));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "Deadline",
                value: new DateTime(2024, 1, 25, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6024));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "Deadline",
                value: new DateTime(2024, 1, 20, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6030));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "Deadline",
                value: new DateTime(2024, 1, 23, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6042));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "Deadline",
                value: new DateTime(2024, 1, 21, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6048));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "Deadline",
                value: new DateTime(2024, 1, 28, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6062));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "Deadline",
                value: new DateTime(2024, 1, 24, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6068));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "Deadline",
                value: new DateTime(2024, 1, 22, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6074));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 14,
                column: "Deadline",
                value: new DateTime(2024, 1, 27, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6080));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 15,
                column: "Deadline",
                value: new DateTime(2024, 1, 25, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6086));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 16,
                column: "Deadline",
                value: new DateTime(2024, 1, 23, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6110));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 17,
                column: "Deadline",
                value: new DateTime(2024, 1, 30, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6116));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 18,
                column: "Deadline",
                value: new DateTime(2024, 1, 22, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6122));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 19,
                column: "Deadline",
                value: new DateTime(2024, 1, 24, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6138));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 20,
                column: "Deadline",
                value: new DateTime(2024, 1, 23, 4, 53, 36, 987, DateTimeKind.Local).AddTicks(6155));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6d0fea85-946f-453a-9897-863f79b652cb", "3b11ba9f-2b09-4b1a-b784-87e0040a2f56" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6d0fea85-946f-453a-9897-863f79b652cb", "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6d0fea85-946f-453a-9897-863f79b652cb", "590201ab-1c71-4d80-8da8-78be2bd3df9a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "311c9a88-fe29-4b7c-a8bb-43aef2f3013c", "b3a5b520-36c2-40dd-9c3a-6223a71f7f7f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "acbda893-a8e4-45f2-b3f9-2a0068b29f57", "bbfcea33-5568-4558-b6c0-9353518b9261" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "311c9a88-fe29-4b7c-a8bb-43aef2f3013c", "f2517e43-07ae-4c0f-8f63-e2481b47a5c7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b11ba9f-2b09-4b1a-b784-87e0040a2f56",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "051ccf30-7b18-4abb-ad7d-e2a2ce2bf33a", "AQAAAAIAAYagAAAAELWfmcYrr5O5zMmIcZEuQc/tZ9O2Oyxy2qd3Q1ghmY5CavbDMNY5Afu3v3ObvB6kAg==", "6e2ccc22-afdb-41e1-8ef7-07ee61d4e270" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4b8914a7-6a92-4dce-ae6c-ee2fdac743d3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65e2dfa2-f709-40c3-8f90-74c229a845ed", "AQAAAAIAAYagAAAAEDpp5K9sIcvwVCsSGIl4sTL4qb/E9qies7+Ah+HBxX4wMDmyuOdGOH0cCoteNqWdpA==", "550af599-29c0-4a42-8946-273ab4d14a59" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "590201ab-1c71-4d80-8da8-78be2bd3df9a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b33c401-2b8c-4f88-8460-679c0978ae95", "AQAAAAIAAYagAAAAEIu2NVpdx+4tMspFXPbmPapRXg84c5e4aJNOW8+hV+MyjOsnuh2I/St+HayIer5CHg==", "c1f84cf0-0c4d-4a7f-b120-4c0dfe8d9006" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3a5b520-36c2-40dd-9c3a-6223a71f7f7f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e1b7f8d-8c8d-41b6-9fa3-a3fe168ca3e2", "AQAAAAIAAYagAAAAEPG1+eJJopWcIjnVgCjFWpccxqoc7Fy0K8IQ04a2HNZSGAw5O9IretlEjUutWxxZ7Q==", "ab97223b-40ef-410a-84e6-5c21b9ec23a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbfcea33-5568-4558-b6c0-9353518b9261",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc9939d9-a392-4eeb-be45-54b22143edce", "AQAAAAIAAYagAAAAEEcY5SjyngP3QjfCH6oOe0dIEp658axYsFgMaBoJWNXE+nvA6gSzdykevB9SXZp/ww==", "275ec1a0-09b6-41f0-828c-b072c0c52114" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f2517e43-07ae-4c0f-8f63-e2481b47a5c7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0c1a2d6-4fa9-4daa-af74-a83a13c22263", "AQAAAAIAAYagAAAAEOkGKViacqxMcB53cL1AwEzhnoXYGzT/G5JOwuDzR/i75j9LM8bOwpaOfGsqiXsM+w==", "36e35e19-6c6b-4880-b639-caca460b67aa" });

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deadline",
                value: new DateTime(2024, 1, 23, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5283));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deadline",
                value: new DateTime(2024, 1, 26, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5372));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deadline",
                value: new DateTime(2024, 1, 21, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5376));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "Deadline",
                value: new DateTime(2024, 1, 19, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5380));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "Deadline",
                value: new DateTime(2024, 1, 24, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5383));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "Deadline",
                value: new DateTime(2024, 1, 22, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5391));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "Deadline",
                value: new DateTime(2024, 1, 25, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5405));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "Deadline",
                value: new DateTime(2024, 1, 20, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5409));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "Deadline",
                value: new DateTime(2024, 1, 23, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5417));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "Deadline",
                value: new DateTime(2024, 1, 21, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5420));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "Deadline",
                value: new DateTime(2024, 1, 28, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5427));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "Deadline",
                value: new DateTime(2024, 1, 24, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5431));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "Deadline",
                value: new DateTime(2024, 1, 22, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5435));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 14,
                column: "Deadline",
                value: new DateTime(2024, 1, 27, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 15,
                column: "Deadline",
                value: new DateTime(2024, 1, 25, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5443));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 16,
                column: "Deadline",
                value: new DateTime(2024, 1, 23, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5466));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 17,
                column: "Deadline",
                value: new DateTime(2024, 1, 30, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5470));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 18,
                column: "Deadline",
                value: new DateTime(2024, 1, 22, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5473));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 19,
                column: "Deadline",
                value: new DateTime(2024, 1, 24, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5481));

            migrationBuilder.UpdateData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: 20,
                column: "Deadline",
                value: new DateTime(2024, 1, 23, 4, 49, 43, 815, DateTimeKind.Local).AddTicks(5493));
        }
    }
}
