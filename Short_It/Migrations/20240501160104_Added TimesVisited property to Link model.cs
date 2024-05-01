using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Short_It.Migrations
{
    /// <inheritdoc />
    public partial class AddedTimesVisitedpropertytoLinkmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimesVisited",
                table: "Links",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesVisited",
                table: "Links");
        }
    }
}
