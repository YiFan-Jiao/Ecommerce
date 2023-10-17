using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class orderEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countrys_Orders_OrderId",
                table: "Countrys");

            migrationBuilder.DropIndex(
                name: "IX_Countrys_OrderId",
                table: "Countrys");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Countrys");

            migrationBuilder.RenameColumn(
                name: "totalPriceWithTaxes",
                table: "Orders",
                newName: "TotalPriceWithTaxes");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPriceWithTaxes",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MailingCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryCountry",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryCountry",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TotalPriceWithTaxes",
                table: "Orders",
                newName: "totalPriceWithTaxes");

            migrationBuilder.AlterColumn<decimal>(
                name: "totalPriceWithTaxes",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "MailingCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Countrys",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countrys_OrderId",
                table: "Countrys",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Countrys_Orders_OrderId",
                table: "Countrys",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
