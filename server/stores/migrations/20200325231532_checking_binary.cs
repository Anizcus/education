using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.stores.migrations
{
    public partial class checking_binary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "salt",
                table: "user",
                type: "tinyblob",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "password",
                table: "user",
                type: "tinyblob",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "salt",
                table: "user",
                type: "varchar(128)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "tinyblob");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "user",
                type: "varchar(128)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "tinyblob");
        }
    }
}
