using Microsoft.EntityFrameworkCore.Migrations;

namespace server.stores.migrations
{
    public partial class permission_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_permission_permission_permission_id",
                table: "role_permission");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permission_role_role_id",
                table: "role_permission");

            migrationBuilder.CreateIndex(
                name: "INDEX_ROLE_PERMISSION_ID",
                table: "role_permission",
                columns: new[] { "role_id", "permission_id" });

            migrationBuilder.AddForeignKey(
                name: "FOREIGN_ROLE_PERMISSION_PERMISSION_IDX",
                table: "role_permission",
                column: "permission_id",
                principalTable: "permission",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FOREIGN_ROLE_PERMISSION_ROLE_IDX",
                table: "role_permission",
                column: "role_id",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FOREIGN_ROLE_PERMISSION_PERMISSION_IDX",
                table: "role_permission");

            migrationBuilder.DropForeignKey(
                name: "FOREIGN_ROLE_PERMISSION_ROLE_IDX",
                table: "role_permission");

            migrationBuilder.DropIndex(
                name: "INDEX_ROLE_PERMISSION_ID",
                table: "role_permission");

            migrationBuilder.AddForeignKey(
                name: "FK_role_permission_permission_permission_id",
                table: "role_permission",
                column: "permission_id",
                principalTable: "permission",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permission_role_role_id",
                table: "role_permission",
                column: "role_id",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
