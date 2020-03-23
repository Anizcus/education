using Microsoft.EntityFrameworkCore.Migrations;

namespace server.stores.migrations
{
    public partial class role_key_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_role_RoleId",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "user",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_RoleId",
                table: "user",
                newName: "IX_user_role_id");

            migrationBuilder.AlterColumn<uint>(
                name: "role_id",
                table: "user",
                type: "int(10) unsigned",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int(10) unsigned",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FOREIGN_USER_ROLE_ID",
                table: "user",
                column: "role_id",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FOREIGN_USER_ROLE_ID",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "user",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_id",
                table: "user",
                newName: "IX_user_RoleId");

            migrationBuilder.AlterColumn<uint>(
                name: "RoleId",
                table: "user",
                type: "int(10) unsigned",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "int(10) unsigned");

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_RoleId",
                table: "user",
                column: "RoleId",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
