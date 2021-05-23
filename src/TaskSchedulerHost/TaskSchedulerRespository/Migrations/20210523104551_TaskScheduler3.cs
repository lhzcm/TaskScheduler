using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedulerRespository.Migrations
{
    public partial class TaskScheduler3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "t_task",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "t_task");
        }
    }
}
