using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerFIZ.Migrations
{
    public partial class wdwdwd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("769f41bd-ccd4-45ba-abbd-550ccd0b62e3"),
                columns: new[] { "CreateDate", "PasswordHash" },
                values: new object[] { new DateTime(2024, 5, 25, 19, 25, 0, 831, DateTimeKind.Local).AddTicks(7787), "AQAAAAEAACcQAAAAEC02+mMQW6+rg+UrBTBisN9nmVlnSh1HwjS/KPn7fJ9zw6RICYre21DT0oszolfWcg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("769f41bd-ccd4-45ba-abbd-550ccd0b62e3"),
                columns: new[] { "CreateDate", "PasswordHash" },
                values: new object[] { new DateTime(2024, 5, 25, 19, 19, 16, 310, DateTimeKind.Local).AddTicks(9364), "AQAAAAEAACcQAAAAEPIFgrI6w93cIlYXpAIDslRZQO1OAPkTu1qBvm+bB+hf5XHLQUqJr6vYnuTLntlcMQ==" });
        }
    }
}
