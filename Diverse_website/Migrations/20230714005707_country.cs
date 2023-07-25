using Microsoft.EntityFrameworkCore.Migrations;

namespace Diverse_website.Migrations
{
    public partial class country : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CounrtyId",
                table: "Vendors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CounrtyId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CounrtyId",
                table: "blogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_CounrtyId",
                table: "Vendors",
                column: "CounrtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CounrtyId",
                table: "Projects",
                column: "CounrtyId");

            migrationBuilder.CreateIndex(
                name: "IX_blogs_CounrtyId",
                table: "blogs",
                column: "CounrtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_blogs_Counrties_CounrtyId",
                table: "blogs",
                column: "CounrtyId",
                principalTable: "Counrties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Counrties_CounrtyId",
                table: "Projects",
                column: "CounrtyId",
                principalTable: "Counrties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Counrties_CounrtyId",
                table: "Vendors",
                column: "CounrtyId",
                principalTable: "Counrties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blogs_Counrties_CounrtyId",
                table: "blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Counrties_CounrtyId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Counrties_CounrtyId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_CounrtyId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CounrtyId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_blogs_CounrtyId",
                table: "blogs");

            migrationBuilder.DropColumn(
                name: "CounrtyId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CounrtyId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CounrtyId",
                table: "blogs");
        }
    }
}
