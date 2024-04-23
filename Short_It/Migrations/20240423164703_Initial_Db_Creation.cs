using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Short_It.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Db_Creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LinkTitle = table.Column<string>(type: "TEXT", nullable: false),
                    FullLink = table.Column<string>(type: "TEXT", nullable: false),
                    ShortLink = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");
        }
    }
}
