using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zuroWa.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToMovieAndHabit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Movies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Habits",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Habits");
        }
    }
}
