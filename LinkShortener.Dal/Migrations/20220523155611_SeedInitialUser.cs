using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkShortener.Dal.Migrations
{
    public partial class SeedInitialUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "login", "password" },
                values: new object[] { 1, "initialUser", "pass" });

            migrationBuilder.InsertData(
                table: "balance",
                columns: new[] { "OwnerId", "balance" },
                values: new object[] { 1, 1000m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "balance",
                keyColumn: "OwnerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
