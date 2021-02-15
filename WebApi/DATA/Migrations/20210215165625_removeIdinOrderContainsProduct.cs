using Microsoft.EntityFrameworkCore.Migrations;

namespace DATA.Migrations
{
    public partial class removeIdinOrderContainsProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderContainsProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderContainsProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
