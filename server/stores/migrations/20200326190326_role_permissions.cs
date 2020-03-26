using Microsoft.EntityFrameworkCore.Migrations;

namespace server.stores.migrations
{
    public partial class role_permissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "role_permission",
                columns: new[] { "role_id", "permission_id", "update_time" },
                values: new object[,]
                {
                    { 1u, 1u, null },
                    { 1u, 2u, null },
                    { 1u, 3u, null },
                    { 1u, 4u, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "role_id", "permission_id" },
                keyValues: new object[] { 1u, 1u });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "role_id", "permission_id" },
                keyValues: new object[] { 1u, 2u });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "role_id", "permission_id" },
                keyValues: new object[] { 1u, 3u });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "role_id", "permission_id" },
                keyValues: new object[] { 1u, 4u });
        }
    }
}
