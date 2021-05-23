using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedulerRespository.Migrations
{
    public partial class TaskScheduler2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("t_log");
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

            migrationBuilder.DropTable("t_task");
            migrationBuilder.CreateTable(
                name: "t_task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false),
                    ExecFile = table.Column<string>(type: "varchar(512)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    WriteTime = table.Column<DateTime>(type: "datetime", nullable: false)
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
