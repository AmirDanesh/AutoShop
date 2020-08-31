using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShopping.Migrations
{
    public partial class AddFileNameToCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileNameImage",
                table: "Cars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileNameImage",
                table: "Cars");
        }
    }
}
