using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedulerRepository.Migrations
{
    public partial class TaskScheduler5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_task_command",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(128)", nullable: false),
                    Command = table.Column<string>(type: "varchar(64)", nullable: false),
                    WriteTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_task_command", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_task_command_TaskId",
                table: "t_task_command",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_task_command");
        }
    }
}
