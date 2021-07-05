using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedulerRepository.Migrations
{
    public partial class TaskScheduler7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_task_config",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "varchar(256)", nullable: false),
                    Value = table.Column<string>(type: "varchar(2048)", nullable: false),
                    WriteTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_task_config", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_task_config_TaskId",
                table: "t_task_config",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_task_config");
        }
    }
}
