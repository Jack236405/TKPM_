using Microsoft.EntityFrameworkCore.Migrations;

namespace BanSach.Data.Migrations
{
    public partial class InitialCreate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dongia",
                table: "Sach",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dongia",
                table: "Sach");
        }
    }
}
