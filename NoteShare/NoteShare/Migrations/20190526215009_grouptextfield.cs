using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteShare.Migrations
{
    public partial class grouptextfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Groups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Groups");
        }
    }
}
