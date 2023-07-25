using Microsoft.EntityFrameworkCore.Migrations;

namespace Diverse_website.Migrations
{
    public partial class addcountries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CounrtId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CounrtId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CounrtId",
                table: "blogs");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "blogs",
                newName: "userid");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Vendors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "blogs");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "blogs",
                newName: "Userid");

            migrationBuilder.AddColumn<int>(
                name: "CounrtId",
                table: "Vendors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CounrtId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CounrtId",
                table: "blogs",
                type: "int",
                nullable: true);
        }
    }
}
