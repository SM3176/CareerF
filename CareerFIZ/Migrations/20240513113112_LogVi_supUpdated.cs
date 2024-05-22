using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerFIZ.Migrations
{
    public partial class LogVi_supUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("376c1d1e-0b04-47da-9657-a2a87faf0a59"),
                column: "ConcurrencyStamp",
                value: "b0f3a700-d0f2-4109-8719-5e6659866cc5");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("4e233be7-c199-4567-9c07-9271a9de4c64"),
                column: "ConcurrencyStamp",
                value: "789432d8-3e65-4210-acd8-57b6b7bd9ed2");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f685d0f-bd6f-44dd-ab60-c606952eb2a8"),
                column: "ConcurrencyStamp",
                value: "637c5128-ab7a-460d-92e2-19b6e226ea20");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("769f41bd-ccd4-45ba-abbd-550ccd0b62e3"),
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash" },
                values: new object[] { "8fb0afd3-95ba-47b8-957f-156a4150a2a0", new DateTime(2024, 5, 13, 14, 31, 12, 136, DateTimeKind.Local).AddTicks(6793), "AQAAAAEAACcQAAAAEBy5F/0+Wi4iFH1hgLW/Rq+8opMX1gMJJELlwPcpL2gm9Quef1QBA1yfsQh+8MGL3Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("376c1d1e-0b04-47da-9657-a2a87faf0a59"),
                column: "ConcurrencyStamp",
                value: "10532965-57e8-45c4-adb1-44915c7adbcf");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("4e233be7-c199-4567-9c07-9271a9de4c64"),
                column: "ConcurrencyStamp",
                value: "8aa4b618-f34b-4f95-9cf3-013761ad99dc");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f685d0f-bd6f-44dd-ab60-c606952eb2a8"),
                column: "ConcurrencyStamp",
                value: "a3b69850-d83b-424c-a417-e579620461b2");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("769f41bd-ccd4-45ba-abbd-550ccd0b62e3"),
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash" },
                values: new object[] { "b65f6be0-1e70-4cbe-81b4-a43b7beafb38", new DateTime(2024, 5, 13, 14, 20, 41, 67, DateTimeKind.Local).AddTicks(1063), "AQAAAAEAACcQAAAAEJa3pAwYQsSwUxYoMJ3SZZqqPmu6UE6geyfWP1clBsarQ9i8T/c4WA3v1tFRStj9Eg==" });
        }
    }
}
