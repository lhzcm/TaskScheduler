using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedulerRepository.Migrations
{
    public partial class InitialCreate_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "t_user_info",
                type: "char(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(18)");

            migrationBuilder.AddColumn<DateTime>(
                name: "WriteTime",
                table: "t_user_info",
                type: "datetime",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WriteTime",
                table: "t_user_info");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "t_user_info",
                type: "varchar(18)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(32)");
        }
    }
}
