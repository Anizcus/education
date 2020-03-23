using Microsoft.EntityFrameworkCore.Migrations;

namespace server.stores.migrations
{
    public partial class role_index_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_user_role_id",
                table: "user",
                newName: "INDEX_USER_ROLE_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "INDEX_USER_ROLE_ID",
                table: "user",
                newName: "IX_user_role_id");
        }
    }
}
