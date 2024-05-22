using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerFIZ.Migrations
{
    public partial class LogVi_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Log_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Log_AppUserId",
                table: "Log",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");

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
    }
}
