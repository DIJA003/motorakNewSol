using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace motorak.dal.Migrations
{
    /// <inheritdoc />
    public partial class edittheCarItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartItems");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CarId",
                table: "CartItems",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Cars_CarId",
                table: "CartItems",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Cars_CarId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CarId",
                table: "CartItems");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
