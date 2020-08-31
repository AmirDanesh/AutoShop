using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShopping.Migrations
{
    public partial class OnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandID",
                table: "Models");

            migrationBuilder.AlterColumn<string>(
                name: "LogoNameImg",
                table: "Brands",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nVarChar(40)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandID",
                table: "Models",
                column: "BrandID",
                principalTable: "Brands",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandID",
                table: "Models");

            migrationBuilder.AlterColumn<string>(
                name: "LogoNameImg",
                table: "Brands",
                type: "nVarChar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandID",
                table: "Models",
                column: "BrandID",
                principalTable: "Brands",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
