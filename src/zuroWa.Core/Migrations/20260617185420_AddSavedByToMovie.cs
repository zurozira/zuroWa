using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zuroWa.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddSavedByToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SavedBy",
                table: "Movies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SavedBy",
                table: "Movies");
        }
    }
}
