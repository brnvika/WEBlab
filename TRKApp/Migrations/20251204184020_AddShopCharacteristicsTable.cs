using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TRKApp.Migrations
{
    /// <inheritdoc />
    public partial class AddShopCharacteristicsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopAssortiment",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "ShopCollection",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "ShopLocation",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "ShopPrice",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "ShopQuality",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "ShopStyle",
                table: "Shops");

            migrationBuilder.CreateTable(
                name: "ShopCharacteristics",
                columns: table => new
                {
                    CharacteristicId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShopId = table.Column<Guid>(type: "uuid", nullable: false),
                    Parameter = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopCharacteristics", x => x.CharacteristicId);
                    table.ForeignKey(
                        name: "FK_ShopCharacteristics_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopCharacteristics_ShopId",
                table: "ShopCharacteristics",
                column: "ShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopCharacteristics");

            migrationBuilder.AddColumn<string>(
                name: "ShopAssortiment",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShopCollection",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShopLocation",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShopPrice",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShopQuality",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShopStyle",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
