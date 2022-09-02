using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wemuda_book_app.Migrations
{
    public partial class bookStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookStatus",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookStatus",
                table: "Books");
        }
    }
}
