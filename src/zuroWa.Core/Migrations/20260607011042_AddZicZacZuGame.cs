using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zuroWa.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddZicZacZuGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerXCode = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerOCode = table.Column<string>(type: "TEXT", nullable: false),
                    BoardState = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerTurn = table.Column<char>(type: "TEXT", nullable: false),
                    Winner = table.Column<char>(type: "TEXT", nullable: true),
                    GameStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
