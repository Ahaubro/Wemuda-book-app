using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wemuda_book_app.Migrations
{
    public partial class addingBooksRead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(name: "BooksRead", table: "Books", defaultValue: 0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("BooksRead", "Books");
        }
    }
}
