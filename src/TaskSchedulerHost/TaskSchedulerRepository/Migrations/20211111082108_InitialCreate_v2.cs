using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedulerRepository.Migrations
{
    public partial class InitialCreate_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "t_user_info",
                type: "varchar(32)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "t_user_info");
        }
    }
}
