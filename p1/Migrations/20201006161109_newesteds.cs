using Microsoft.EntityFrameworkCore.Migrations;

namespace p1.Migrations
{
    public partial class newesteds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderDetails_DetailsOrderDetailsId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_DetailsOrderDetailsId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DetailsOrderDetailsId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "DetailsOrderDetailsId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_DetailsOrderDetailsId",
                table: "Order",
                column: "DetailsOrderDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderDetails_DetailsOrderDetailsId",
                table: "Order",
                column: "DetailsOrderDetailsId",
                principalTable: "OrderDetails",
                principalColumn: "OrderDetailsId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
