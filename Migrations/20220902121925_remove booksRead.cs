using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wemuda_book_app.Migrations
{
    public partial class removebooksRead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "BooksRead", table: "Books");
            migrationBuilder.AddColumn<int>(name: "BooksRead", table: "Users", defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
