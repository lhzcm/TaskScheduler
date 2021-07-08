using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedulerHost.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "varchar(max)", nullable: false),
                    WriteTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false),
                    ExecFile = table.Column<string>(type: "varchar(512)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WriteTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_task", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_task_command",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(128)", nullable: false),
                    Command = table.Column<string>(type: "varchar(64)", nullable: false),
                    WriteTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_task_command", x => x.Id);
                });

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
                name: "IX_t_task_command_TaskId",
                table: "t_task_command",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_t_task_config_TaskId",
                table: "t_task_config",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_log");

            migrationBuilder.DropTable(
                name: "t_task");

            migrationBuilder.DropTable(
                name: "t_task_command");

            migrationBuilder.DropTable(
                name: "t_task_config");
        }
    }
}
