using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedulerRepository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_log",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskId = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskGuid = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false),
                    ExecFile = table.Column<string>(type: "varchar(512)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Status = table.Column<int>(nullable: false),
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskId = table.Column<int>(nullable: false),
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskId = table.Column<int>(nullable: false),
                    Key = table.Column<string>(type: "varchar(256)", nullable: false),
                    Value = table.Column<string>(type: "varchar(2048)", nullable: false),
                    WriteTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_task_config", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_task_manage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    Access = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_task_manage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_user_info",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Password = table.Column<string>(type: "varchar(18)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_info", x => x.Id);
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

            migrationBuilder.DropTable(
                name: "t_task_manage");

            migrationBuilder.DropTable(
                name: "t_user_info");
        }
    }
}
