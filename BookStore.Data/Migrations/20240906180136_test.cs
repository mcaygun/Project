using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "94f5dfcc-071a-41ce-83d0-a7e34c7e0e0f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc34db48-a2ea-4674-8a07-2aa4136edb70", new DateTime(2024, 9, 6, 21, 1, 35, 780, DateTimeKind.Local).AddTicks(9000), "AQAAAAIAAYagAAAAEBpPFEoOujSq6ZtVPSS9iMYuOD2Nm13J1MpPBernLSzEWnaluwEqPzMvbYAjC12wIw==", "1fd248c9-3c17-4577-b0c2-f4770a9a7669" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3d21db51-6428-4b6d-a077-1937f56905a8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d8f75eb-e19b-4957-a8db-9595ff1921ac", new DateTime(2024, 9, 6, 13, 34, 0, 341, DateTimeKind.Local).AddTicks(4456), "AQAAAAIAAYagAAAAEC/sSHAzq3UcnfkxKmB+d1hPR9ZXRdg2x6jJlfnoF03OScUSj2iNwN7JD9EqkIiJmQ==", "7a608d17-109f-4a92-a59c-e7c8ac1ff79c" });
        }
    }
}
