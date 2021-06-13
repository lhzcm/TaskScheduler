using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedulerRepository.Migrations
{
    public partial class TaskScheduler6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "WriteTime",
                table: "t_task_command",
                type: "datetime",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "WriteTime",
                table: "t_task_command",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "getdate()");
        }
    }
}
