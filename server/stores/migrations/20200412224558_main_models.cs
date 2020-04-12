using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.stores.migrations
{
    public partial class main_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(32)", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: true, defaultValueSql: "NULL ON UPDATE CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "progress",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(32)", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: true, defaultValueSql: "NULL ON UPDATE CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_progress", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "state",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(32)", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: true, defaultValueSql: "NULL ON UPDATE CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(32)", nullable: false),
                    category_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: true, defaultValueSql: "NULL ON UPDATE CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type", x => x.id);
                    table.ForeignKey(
                        name: "FOREIGN_TYPE_CATEGORY_ID",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    state_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    description = table.Column<string>(type: "varchar(32)", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
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

            migrationBuilder.CreateTable(
                name: "lesson",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(64)", nullable: true),
                    description = table.Column<string>(type: "varchar(256)", nullable: false),
                    badge = table.Column<byte[]>(type: "tinyblob", nullable: true),
                    status_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    category_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    type_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    owner_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: true, defaultValueSql: "NULL ON UPDATE CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson", x => x.id);
                    table.ForeignKey(
                        name: "FOREIGN_LESSON_CATEGORY_ID",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FOREIGN_LESSON_OWNER_ID",
                        column: x => x.owner_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FOREIGN_LESSON_STATUS_ID",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FOREIGN_LESSON_TYPE_ID",
                        column: x => x.type_id,
                        principalTable: "type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assignment",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(type: "varchar(256)", nullable: false),
                    experience = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: true, defaultValueSql: "NULL ON UPDATE CURRENT_TIMESTAMP"),
                    lesson_id = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignment", x => x.id);
                    table.ForeignKey(
                        name: "FOREIGN_ASSIGNMENT_LESSON_ID",
                        column: x => x.lesson_id,
                        principalTable: "lesson",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_lesson",
                columns: table => new
                {
                    user_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    lesson_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    progress_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: true, defaultValueSql: "NULL ON UPDATE CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("INDEX_USER_LESSON_ID", x => new { x.user_id, x.lesson_id });
                    table.ForeignKey(
                        name: "FOREIGN_USER_LESSON_LESSON_ID",
                        column: x => x.lesson_id,
                        principalTable: "lesson",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FOREIGN_USER_LESSON_PROGRESS_ID",
                        column: x => x.progress_id,
                        principalTable: "progress",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FOREIGN_USER_LESSON_USER_ID",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_assignment",
                columns: table => new
                {
                    user_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    assignment_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    progress_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp", nullable: true, defaultValueSql: "NULL ON UPDATE CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("INDEX_USER_ASSIGNMENT_ID", x => new { x.user_id, x.assignment_id });
                    table.ForeignKey(
                        name: "FOREIGN_USER_ASSIGNMENT_ASSIGNMENT_ID",
                        column: x => x.assignment_id,
                        principalTable: "assignment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FOREIGN_USER_ASSIGNMENT_PROGRESS_ID",
                        column: x => x.progress_id,
                        principalTable: "progress",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FOREIGN_USER_ASSIGNMENT_USER_ID",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "id", "name", "update_time" },
                values: new object[,]
                {
                    { 1u, "Language", null },
                    { 2u, "Programming", null },
                    { 3u, "Science", null }
                });

            migrationBuilder.InsertData(
                table: "progress",
                columns: new[] { "id", "name", "update_time" },
                values: new object[,]
                {
                    { 1u, "Active", null },
                    { 2u, "Paused", null },
                    { 3u, "Completed", null }
                });

            migrationBuilder.InsertData(
                table: "state",
                columns: new[] { "id", "name", "update_time" },
                values: new object[,]
                {
                    { 1u, "Waiting", null },
                    { 2u, "Accepted", null },
                    { 3u, "Rejected", null }
                });

            migrationBuilder.InsertData(
                table: "type",
                columns: new[] { "id", "category_id", "name", "update_time" },
                values: new object[] { 2u, 1u, "English", null });

            migrationBuilder.InsertData(
                table: "type",
                columns: new[] { "id", "category_id", "name", "update_time" },
                values: new object[] { 3u, 1u, "Russian", null });

            migrationBuilder.InsertData(
                table: "type",
                columns: new[] { "id", "category_id", "name", "update_time" },
                values: new object[] { 1u, 3u, "Math", null });

            migrationBuilder.CreateIndex(
                name: "INDEX_ASSIGNMENT_LESSON_ID",
                table: "assignment",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "INDEX_LESSON_CATEGORY_ID",
                table: "lesson",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "INDEX_LESSON_OWNER_ID",
                table: "lesson",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "INDEX_LESSON_STATUS_ID",
                table: "lesson",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "INDEX_LESSON_TYPE_ID",
                table: "lesson",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "INDEX_STATUS_STATE_ID",
                table: "status",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "INDEX_TYPE_CATEGORY_ID",
                table: "type",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "INDEX_USER_ASSIGNMENT_ASSIGNMENT_ID",
                table: "user_assignment",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "INDEX_USER_ASSIGNMENT_PROGRESS_ID",
                table: "user_assignment",
                column: "progress_id");

            migrationBuilder.CreateIndex(
                name: "INDEX_USER_ASSIGNMENT_USER_ID",
                table: "user_assignment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "INDEX_USER_LESSON_LESSON_ID",
                table: "user_lesson",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "INDEX_USER_LESSON_PROGRESS_ID",
                table: "user_lesson",
                column: "progress_id");

            migrationBuilder.CreateIndex(
                name: "INDEX_USER_LESSON_USER_ID",
                table: "user_lesson",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_assignment");

            migrationBuilder.DropTable(
                name: "user_lesson");

            migrationBuilder.DropTable(
                name: "assignment");

            migrationBuilder.DropTable(
                name: "progress");

            migrationBuilder.DropTable(
                name: "lesson");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "type");

            migrationBuilder.DropTable(
                name: "state");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}
