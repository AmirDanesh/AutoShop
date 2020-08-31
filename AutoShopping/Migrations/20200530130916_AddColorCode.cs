using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShopping.Migrations
{
    public partial class AddColorCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ColorName",
                table: "Colors",
                type: "nVarChar(10)",
                nullable: false,
                defaultValue: "#000000",
                oldClrType: typeof(string),
                oldType: "nVarChar(10)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ColorName",
                table: "Colors",
                type: "nVarChar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nVarChar(10)",
                oldDefaultValue: "#000000");
        }
    }
}
