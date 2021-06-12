using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedulerRespository.Migrations
{
    public partial class TaskScheduler2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "WriteTime",
                table: "t_log",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2021, 5, 24, 16, 29, 43, 127, DateTimeKind.Local).AddTicks(2217)
                );
                
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "WriteTime",
                table: "t_log",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 24, 16, 29, 43, 127, DateTimeKind.Local).AddTicks(2217),
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
