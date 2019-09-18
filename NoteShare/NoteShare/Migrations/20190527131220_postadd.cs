using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteShare.Migrations
{
    public partial class postadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Post",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupOwnerId",
                table: "Groups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "GroupOwnerId",
                table: "Groups");
        }
    }
}
