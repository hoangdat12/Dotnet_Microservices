using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhileLagoon.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateTableInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Inventories",
                newName: "ShopId");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Inventories",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Inventories");

            migrationBuilder.RenameColumn(
                name: "ShopId",
                table: "Inventories",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories",
                column: "Id");
        }
    }
}
