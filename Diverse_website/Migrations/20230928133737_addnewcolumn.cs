using Microsoft.EntityFrameworkCore.Migrations;

namespace Diverse_website.Migrations
{
    public partial class addnewcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "DescFr",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleFr",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

          

            migrationBuilder.AddColumn<string>(
                name: "DescFr",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleFr",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

      

            migrationBuilder.AddColumn<string>(
                name: "ContentFr",
                table: "blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleFr",
                table: "blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
