using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class modelsCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countrys_Order_OrderId",
                table: "Countrys");

            migrationBuilder.DropIndex(
                name: "IX_Countrys_OrderId",
                table: "Countrys");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Countrys",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Countrys_OrderId",
                table: "Countrys",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Countrys_Order_OrderId",
                table: "Countrys",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countrys_Order_OrderId",
                table: "Countrys");

            migrationBuilder.DropIndex(
                name: "IX_Countrys_OrderId",
                table: "Countrys");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Countrys",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countrys_OrderId",
                table: "Countrys",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Countrys_Order_OrderId",
                table: "Countrys",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
