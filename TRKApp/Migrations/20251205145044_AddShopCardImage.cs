using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TRKApp.Migrations
{
    /// <inheritdoc />
    public partial class AddShopCardImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShopCardImage",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopCardImage",
                table: "Shops");
        }
    }
}
