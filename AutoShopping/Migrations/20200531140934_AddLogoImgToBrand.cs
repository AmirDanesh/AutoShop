using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShopping.Migrations
{
    public partial class AddLogoImgToBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoNameImg",
                table: "Brands",
                type: "nVarChar(40)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoNameImg",
                table: "Brands");
        }
    }
}
