using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerFIZ.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_AppUsers_AppUserId",
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
                value: "e4151974-d25e-4722-b0ca-96cdafbcdb53");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("4e233be7-c199-4567-9c07-9271a9de4c64"),
                column: "ConcurrencyStamp",
                value: "a8766a5b-69ba-43aa-bafd-bf6b849d913f");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f685d0f-bd6f-44dd-ab60-c606952eb2a8"),
                column: "ConcurrencyStamp",
                value: "58c0b45d-8a39-4ec0-bd70-15d101f8bfbc");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("769f41bd-ccd4-45ba-abbd-550ccd0b62e3"),
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash" },
                values: new object[] { "7d93635e-f542-4913-bd69-64f7720f62f6", new DateTime(2024, 5, 11, 17, 28, 39, 411, DateTimeKind.Local).AddTicks(287), "AQAAAAEAACcQAAAAEIeCStR+p8nHDIxyMbIiZMf2KqwqIHQxQYqzMv01EOVhhvtnXQFdP5TyAJRNuRnFhw==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "the ability to carry out diverse duties in the industry");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_AppUserId",
                table: "Payment",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Popular = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_AppUsers_AppUserId",
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
                value: "bb760d45-ef93-4c5e-aa36-524ea4960072");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("4e233be7-c199-4567-9c07-9271a9de4c64"),
                column: "ConcurrencyStamp",
                value: "dad3c18c-1340-4da2-88b0-f15c9bf65d22");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f685d0f-bd6f-44dd-ab60-c606952eb2a8"),
                column: "ConcurrencyStamp",
                value: "8d681dff-b911-4aea-bb97-3be17ba95e48");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("769f41bd-ccd4-45ba-abbd-550ccd0b62e3"),
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash" },
                values: new object[] { "e6d2c4d0-3172-4dff-8c92-499dfe6597a0", new DateTime(2023, 7, 8, 2, 55, 16, 64, DateTimeKind.Local).AddTicks(9520), "AQAAAAEAACcQAAAAEAfXXQh97Y9dgVTzTXk4vJqMeZfUhIdwZ/EDrZOayS3pdscyowtYHMs4yu6Mg7ch3w==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "the ability to carry out diverse duties in IT roles");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AppUserId",
                table: "Blogs",
                column: "AppUserId");
        }
    }
}
