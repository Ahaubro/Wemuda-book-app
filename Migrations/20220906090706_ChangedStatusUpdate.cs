using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wemuda_book_app.Migrations
{
    public partial class ChangedStatusUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BooksRead",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrentPage",
                table: "StatusUpdates");

            migrationBuilder.DropColumn(
                name: "FinishedBook",
                table: "StatusUpdates");

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "StatusUpdates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BooksRead",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "StatusUpdates",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPage",
                table: "StatusUpdates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "FinishedBook",
                table: "StatusUpdates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
