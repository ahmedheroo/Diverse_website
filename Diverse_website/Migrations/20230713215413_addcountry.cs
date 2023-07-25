using Microsoft.EntityFrameworkCore.Migrations;

namespace Diverse_website.Migrations
{
    public partial class addcountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userid",
                table: "blogs",
                newName: "Userid");

            migrationBuilder.AddColumn<int>(
                name: "CounrtId",
                table: "Vendors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VendorUrl",
                table: "Vendors",
                type: "nvarchar(max)",
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

            migrationBuilder.CreateTable(
                name: "Counrties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counrties", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Counrties");

            migrationBuilder.DropColumn(
                name: "CounrtId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "VendorUrl",
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
        }
    }
}
