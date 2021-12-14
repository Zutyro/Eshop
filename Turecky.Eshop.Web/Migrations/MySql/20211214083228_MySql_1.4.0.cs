using Microsoft.EntityFrameworkCore.Migrations;

namespace Turecky.Eshop.Web.Migrations.MySql
{
    public partial class MySql_140 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageSrc",
                table: "EshopItem",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSrc",
                table: "EshopItem");
        }
    }
}
