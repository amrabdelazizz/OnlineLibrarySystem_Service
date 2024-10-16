using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLibrarySystem.Migrations
{
    /// <inheritdoc />
    public partial class add_isReturned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "BorrowedBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "BorrowedBooks");
        }
    }
}
