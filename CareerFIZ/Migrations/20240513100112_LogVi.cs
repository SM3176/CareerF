using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerFIZ.Migrations
{
    public partial class LogVi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("379c1d1e-3b50-71da-6875-a3a64faf6a88"));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("376c1d1e-0b04-47da-9657-a2a87faf0a59"),
                column: "ConcurrencyStamp",
                value: "a872fa3d-0e82-41c9-948d-4ea6f8c3700d");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("4e233be7-c199-4567-9c07-9271a9de4c64"),
                column: "ConcurrencyStamp",
                value: "b69ef469-ab19-4d63-b87e-eb06dc487bb9");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f685d0f-bd6f-44dd-ab60-c606952eb2a8"),
                column: "ConcurrencyStamp",
                value: "28e1bca2-648d-4614-adcd-5d7eb4aa6acb");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("769f41bd-ccd4-45ba-abbd-550ccd0b62e3"),
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash" },
                values: new object[] { "7d4534a6-15eb-45c7-a77c-3fb30ecb45bb", new DateTime(2024, 5, 13, 13, 1, 11, 308, DateTimeKind.Local).AddTicks(3329), "AQAAAAEAACcQAAAAEBxzT/g72huHkaIeKjv3BDcnrQxTW4Or9a8WklRz3vEacQiCxx4p2/WQ8F5Jpemuvg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("376c1d1e-0b04-47da-9657-a2a87faf0a59"),
                column: "ConcurrencyStamp",
                value: "58955016-dd5a-4cb2-952b-f8367116c631");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("4e233be7-c199-4567-9c07-9271a9de4c64"),
                column: "ConcurrencyStamp",
                value: "f0c0063a-f131-4181-803f-3e2e31ef1992");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f685d0f-bd6f-44dd-ab60-c606952eb2a8"),
                column: "ConcurrencyStamp",
                value: "aa24433d-04d7-44bf-a0c1-81836d73a2e0");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("379c1d1e-3b50-71da-6875-a3a64faf6a88"), "936d6f92-c10a-47c7-8431-0f90becc02d9", "HRStaff role", "Staff", "HRSTAFF" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("769f41bd-ccd4-45ba-abbd-550ccd0b62e3"),
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash" },
                values: new object[] { "1ced8ab5-2a3a-4ba5-92d4-3d74b9bc5220", new DateTime(2024, 5, 13, 12, 58, 8, 391, DateTimeKind.Local).AddTicks(9557), "AQAAAAEAACcQAAAAEP+pAbWzvGS85sGizm8GUKkVOfYrXjx+i1KttWPNeB2eHKA5Wkdot8f1KV0Zx1bhcA==" });
        }
    }
}
