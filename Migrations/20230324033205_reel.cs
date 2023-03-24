using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proj.Migrations
{
    /// <inheritdoc />
    public partial class reel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reel",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reel",
                table: "Products");
        }
    }
}
