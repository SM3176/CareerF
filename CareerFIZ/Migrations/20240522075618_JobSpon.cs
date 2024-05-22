using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerFIZ.Migrations
{
    public partial class JobSpon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isSponser",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("376c1d1e-0b04-47da-9657-a2a87faf0a59"),
                column: "ConcurrencyStamp",
                value: "0264f295-80ce-44cf-a890-60b6c33cfaf1");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("4e233be7-c199-4567-9c07-9271a9de4c64"),
                column: "ConcurrencyStamp",
                value: "0dd2983e-710b-42c0-9903-eb99e0c6108c");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f685d0f-bd6f-44dd-ab60-c606952eb2a8"),
                column: "ConcurrencyStamp",
                value: "f365fdb1-1e95-4e59-bbbd-0f4633a23ce6");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("769f41bd-ccd4-45ba-abbd-550ccd0b62e3"),
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash" },
                values: new object[] { "827021fe-685a-43d4-9b64-4be784ebbc42", new DateTime(2024, 5, 22, 10, 56, 17, 419, DateTimeKind.Local).AddTicks(4752), "AQAAAAEAACcQAAAAEN/zs1h2QzhcoIt5VXY0aFaILq4IZwQM/9rNVqXJfpfirG2ERVcgR+jCA87ks8PSlQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSponser",
                table: "Jobs");

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
    }
}
