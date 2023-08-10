using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechCareerFinalProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListDetails_Lists_ListId",
                table: "ListDetails");

            migrationBuilder.DropIndex(
                name: "IX_ListDetails_ListId",
                table: "ListDetails");

            migrationBuilder.DropColumn(
                name: "Products",
                table: "ListDetails");

            migrationBuilder.CreateTable(
                name: "ListDetailProduct",
                columns: table => new
                {
                    ListDetailsID = table.Column<int>(type: "int", nullable: false),
                    ProductsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListDetailProduct", x => new { x.ListDetailsID, x.ProductsID });
                    table.ForeignKey(
                        name: "FK_ListDetailProduct_ListDetails_ListDetailsID",
                        column: x => x.ListDetailsID,
                        principalTable: "ListDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListDetailProduct_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListDetailProduct_ProductsID",
                table: "ListDetailProduct",
                column: "ProductsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListDetailProduct");

            migrationBuilder.AddColumn<string>(
                name: "Products",
                table: "ListDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ListDetails_ListId",
                table: "ListDetails",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListDetails_Lists_ListId",
                table: "ListDetails",
                column: "ListId",
                principalTable: "Lists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
