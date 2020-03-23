using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.stores.migrations
{
    public partial class user_timestamps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "user",
                newName: "update_time");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "user",
                newName: "create_time");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "update_time",
                table: "user",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "current_timestamp()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "create_time",
                table: "user",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "current_timestamp()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "update_time",
                table: "user",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "create_time",
                table: "user",
                newName: "Created");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "user",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp",
                oldDefaultValueSql: "current_timestamp()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Created",
                table: "user",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp",
                oldDefaultValueSql: "current_timestamp()");
        }
    }
}
