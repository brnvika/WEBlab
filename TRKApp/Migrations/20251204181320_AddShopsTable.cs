using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TRKApp.Migrations
{
    /// <inheritdoc />
    public partial class AddShopsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    ShopId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShopName = table.Column<string>(type: "text", nullable: false),
                    ShopSait = table.Column<string>(type: "text", nullable: false),
                    ShopDescription = table.Column<string>(type: "text", nullable: false),
                    ShopInformation = table.Column<string>(type: "text", nullable: false),
                    ShopLogo = table.Column<string>(type: "text", nullable: false),
                    ShopTRK = table.Column<string>(type: "text", nullable: false),
                    ShopAdvert = table.Column<string>(type: "text", nullable: false),
                    ShopAssortiment = table.Column<string>(type: "text", nullable: false),
                    ShopStyle = table.Column<string>(type: "text", nullable: false),
                    ShopQuality = table.Column<string>(type: "text", nullable: false),
                    ShopCollection = table.Column<string>(type: "text", nullable: false),
                    ShopPrice = table.Column<string>(type: "text", nullable: false),
                    ShopLocation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.ShopId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shops");
        }
    }
}
