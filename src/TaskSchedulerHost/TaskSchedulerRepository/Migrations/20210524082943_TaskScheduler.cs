using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedulerRepository.Migrations
{
    public partial class TaskScheduler : Migration
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
                    WriteTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: null, defaultValueSql:"getdate()")
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
                    TaskGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: null, defaultValueSql: "newid()"),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false),
                    ExecFile = table.Column<string>(type: "varchar(512)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: null, defaultValueSql: "getdate()"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WriteTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: null, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_task", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_log");

            migrationBuilder.DropTable(
                name: "t_task");
        }
    }
}
