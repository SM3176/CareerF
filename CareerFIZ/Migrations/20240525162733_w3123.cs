using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerFIZ.Migrations
{
    public partial class w3123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isSponser",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("769f41bd-ccd4-45ba-abbd-550ccd0b62e3"),
                columns: new[] { "CreateDate", "PasswordHash" },
                values: new object[] { new DateTime(2024, 5, 25, 19, 27, 33, 106, DateTimeKind.Local).AddTicks(7088), "AQAAAAEAACcQAAAAENyvYEa0Sw2aJvOSMnRNKuqgFWv+0mAlAohKTRJblwvB+xfsviluJPwg5QZsNyHFRg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isSponser",
                table: "Jobs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("769f41bd-ccd4-45ba-abbd-550ccd0b62e3"),
                columns: new[] { "CreateDate", "PasswordHash" },
                values: new object[] { new DateTime(2024, 5, 25, 19, 25, 0, 831, DateTimeKind.Local).AddTicks(7787), "AQAAAAEAACcQAAAAEC02+mMQW6+rg+UrBTBisN9nmVlnSh1HwjS/KPn7fJ9zw6RICYre21DT0oszolfWcg==" });
        }
    }
}
