using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TRKApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDisplayOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "ShopCharacteristics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "ShopCharacteristics",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
