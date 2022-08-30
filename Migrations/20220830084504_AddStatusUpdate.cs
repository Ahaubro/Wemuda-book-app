using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wemuda_book_app.Migrations
{
    public partial class AddStatusUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusUpdates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOfUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinutesRead = table.Column<int>(type: "int", nullable: false),
                    CurrentPage = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    FinishedBook = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusUpdates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusUpdates");
        }
    }
}
