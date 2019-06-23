using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineGroceryStore.Migrations
{
    public partial class InitialContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    itemID = table.Column<string>(nullable: false),
                    itemName = table.Column<string>(nullable: true),
                    itemCode = table.Column<string>(nullable: true),
                    stockLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.itemID);
                });

            migrationBuilder.CreateTable(
                name: "InventoryPackingConfigure",
                columns: table => new
                {
                    packingID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    packSize = table.Column<int>(nullable: false),
                    packPrice = table.Column<double>(nullable: false),
                    itemID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryPackingConfigure", x => x.packingID);
                    table.ForeignKey(
                        name: "FK_InventoryPackingConfigure_Inventory_itemID",
                        column: x => x.itemID,
                        principalTable: "Inventory",
                        principalColumn: "itemID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPackingConfigure_itemID",
                table: "InventoryPackingConfigure",
                column: "itemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryPackingConfigure");

            migrationBuilder.DropTable(
                name: "Inventory");
        }
    }
}
