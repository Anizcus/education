using Microsoft.EntityFrameworkCore.Migrations;

namespace server.stores.migrations
{
    public partial class updatedpermissionsandotherstuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status_id",
                table: "lesson",
                newName: "state_id");

            migrationBuilder.AddColumn<uint>(
                name: "experience",
                table: "user",
                type: "int(10) unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "level",
                table: "user",
                type: "int(10) unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<string>(
                name: "answer",
                table: "assignment",
                type: "varchar(64)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "id", "name", "update_time" },
                values: new object[] { 15u, "Lesson.Authorize", null });

            migrationBuilder.InsertData(
                table: "role_permission",
                columns: new[] { "role_id", "permission_id", "update_time" },
                values: new object[] { 2u, 13u, null });

            migrationBuilder.InsertData(
                table: "role_permission",
                columns: new[] { "role_id", "permission_id", "update_time" },
                values: new object[] { 2u, 12u, null });

            migrationBuilder.InsertData(
                table: "role_permission",
                columns: new[] { "role_id", "permission_id", "update_time" },
                values: new object[] { 3u, 15u, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "role_id", "permission_id" },
                keyValues: new object[] { 2u, 12u });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "role_id", "permission_id" },
                keyValues: new object[] { 2u, 13u });

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumns: new[] { "role_id", "permission_id" },
                keyValues: new object[] { 3u, 15u });

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 15u);

            migrationBuilder.DropColumn(
                name: "experience",
                table: "user");

            migrationBuilder.DropColumn(
                name: "level",
                table: "user");

            migrationBuilder.DropColumn(
                name: "answer",
                table: "assignment");

            migrationBuilder.RenameColumn(
                name: "state_id",
                table: "lesson",
                newName: "status_id");
        }
    }
}
