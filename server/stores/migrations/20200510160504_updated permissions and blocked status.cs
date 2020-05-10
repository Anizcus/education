using Microsoft.EntityFrameworkCore.Migrations;

namespace server.stores.migrations
{
    public partial class updatedpermissionsandblockedstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "blocked",
                table: "user",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "id", "name", "update_time" },
                values: new object[] { 5u, "User.Modify", null });

            migrationBuilder.InsertData(
                table: "role_permission",
                columns: new[] { "role_id", "permission_id", "update_time" },
                values: new object[] { 3u, 5u, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "role_id", "permission_id" },
                keyValues: new object[] { 3u, 5u });

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 5u);

            migrationBuilder.DropColumn(
                name: "blocked",
                table: "user");
        }
    }
}
