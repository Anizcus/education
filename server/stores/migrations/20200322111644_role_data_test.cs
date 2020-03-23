using Microsoft.EntityFrameworkCore.Migrations;

namespace server.stores.migrations
{
    public partial class role_data_test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "name", "update_time" },
                values: new object[] { 1u, "Student", null });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "name", "update_time" },
                values: new object[] { 2u, "Teacher", null });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "name", "update_time" },
                values: new object[] { 3u, "Administrator", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 1u);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 2u);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 3u);
        }
    }
}
