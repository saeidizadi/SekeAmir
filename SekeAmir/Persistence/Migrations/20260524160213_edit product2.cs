using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editproduct2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_Categoryid",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "change",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "price1",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "price2",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "priceLast1",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "priceLast2",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Categoryid",
                table: "Product",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Categoryid",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Product",
                newName: "Categoryid");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                newName: "IX_Product_Categoryid");

            migrationBuilder.AlterColumn<int>(
                name: "Categoryid",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "change",
                table: "Product",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "price1",
                table: "Product",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "price2",
                table: "Product",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "priceLast1",
                table: "Product",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "priceLast2",
                table: "Product",
                type: "float",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_Categoryid",
                table: "Product",
                column: "Categoryid",
                principalTable: "Categories",
                principalColumn: "id");
        }
    }
}
