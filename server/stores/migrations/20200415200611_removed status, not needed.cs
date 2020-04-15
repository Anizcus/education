using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.stores.migrations
{
    public partial class removedstatusnotneeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FOREIGN_LESSON_STATUS_ID",
                table: "lesson");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.RenameIndex(
                name: "INDEX_LESSON_STATUS_ID",
                table: "lesson",
                newName: "INDEX_LESSON_STATE_ID");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "lesson",
                type: "varchar(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "lesson",
                type: "varchar(256)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "state",
                keyColumn: "id",
                keyValue: 1u,
                column: "name",
                value: "Created");

            migrationBuilder.UpdateData(
                table: "state",
                keyColumn: "id",
                keyValue: 2u,
                column: "name",
                value: "Waiting");

            migrationBuilder.UpdateData(
                table: "state",
                keyColumn: "id",
                keyValue: 3u,
                column: "name",
                value: "Published");

            migrationBuilder.InsertData(
                table: "state",
                columns: new[] { "id", "name", "update_time" },
                values: new object[] { 4u, "Rejected", null });

            migrationBuilder.AddForeignKey(
                name: "FOREIGN_LESSON_STATE_ID",
                table: "lesson",
                column: "status_id",
                principalTable: "state",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FOREIGN_LESSON_STATE_ID",
                table: "lesson");

            migrationBuilder.DeleteData(
                table: "state",
                keyColumn: "id",
                keyValue: 4u);

            migrationBuilder.DropColumn(
                name: "status",
                table: "lesson");

            migrationBuilder.RenameIndex(
                name: "INDEX_LESSON_STATE_ID",
                table: "lesson",
                newName: "INDEX_LESSON_STATUS_ID");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "lesson",
                type: "varchar(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)");

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    description = table.Column<string>(type: "varchar(32)", nullable: false),
                    state_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: true, defaultValueSql: "NULL ON UPDATE CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.id);
                    table.ForeignKey(
                        name: "FOREIGN_STATUS_STATE_ID",
                        column: x => x.state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "state",
                keyColumn: "id",
                keyValue: 1u,
                column: "name",
                value: "Waiting");

            migrationBuilder.UpdateData(
                table: "state",
                keyColumn: "id",
                keyValue: 2u,
                column: "name",
                value: "Accepted");

            migrationBuilder.UpdateData(
                table: "state",
                keyColumn: "id",
                keyValue: 3u,
                column: "name",
                value: "Rejected");

            migrationBuilder.CreateIndex(
                name: "INDEX_STATUS_STATE_ID",
                table: "status",
                column: "state_id");

            migrationBuilder.AddForeignKey(
                name: "FOREIGN_LESSON_STATUS_ID",
                table: "lesson",
                column: "status_id",
                principalTable: "status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
