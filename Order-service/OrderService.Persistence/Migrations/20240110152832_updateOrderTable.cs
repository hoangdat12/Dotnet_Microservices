using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discount",
                table: "OrderCheckouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShopDiscount",
                table: "OrderCheckouts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "OrderCheckouts");

            migrationBuilder.DropColumn(
                name: "ShopDiscount",
                table: "OrderCheckouts");
        }
    }
}
