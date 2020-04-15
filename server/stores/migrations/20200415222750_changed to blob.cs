using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.stores.migrations
{
    public partial class changedtoblob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "badge",
                table: "lesson",
                type: "blob",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "tinyblob",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "badge",
                table: "lesson",
                type: "tinyblob",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "blob",
                oldNullable: true);
        }
    }
}
