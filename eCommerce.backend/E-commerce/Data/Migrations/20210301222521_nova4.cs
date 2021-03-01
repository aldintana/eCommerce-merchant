using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class nova4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenderSubCategory_SubCategory_SubCategoryID",
                table: "GenderSubCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_ItemSize_ItemSizeID",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_ItemSize_ItemSizeID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_ItemSize_ItemSizeID",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_ItemSize_ItemSizeID",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategory",
                table: "SubCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemSize",
                table: "ItemSize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemCostHistory",
                table: "ItemCostHistory");

            migrationBuilder.DropColumn(
                name: "SubCategoryID",
                table: "SubCategory");

            migrationBuilder.DropColumn(
                name: "ItemSizeID",
                table: "ItemSize");

            migrationBuilder.DropColumn(
                name: "ItemCostHistoryID",
                table: "ItemCostHistory");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "SubCategory",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ItemSize",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ItemCostHistory",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategory",
                table: "SubCategory",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemSize",
                table: "ItemSize",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemCostHistory",
                table: "ItemCostHistory",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_GenderSubCategory_SubCategory_SubCategoryID",
                table: "GenderSubCategory",
                column: "SubCategoryID",
                principalTable: "SubCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_ItemSize_ItemSizeID",
                table: "Inventory",
                column: "ItemSizeID",
                principalTable: "ItemSize",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ItemSize_ItemSizeID",
                table: "Order",
                column: "ItemSizeID",
                principalTable: "ItemSize",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_ItemSize_ItemSizeID",
                table: "Purchase",
                column: "ItemSizeID",
                principalTable: "ItemSize",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_ItemSize_ItemSizeID",
                table: "Warehouse",
                column: "ItemSizeID",
                principalTable: "ItemSize",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenderSubCategory_SubCategory_SubCategoryID",
                table: "GenderSubCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_ItemSize_ItemSizeID",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_ItemSize_ItemSizeID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_ItemSize_ItemSizeID",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_ItemSize_ItemSizeID",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategory",
                table: "SubCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemSize",
                table: "ItemSize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemCostHistory",
                table: "ItemCostHistory");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "SubCategory");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ItemSize");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ItemCostHistory");

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryID",
                table: "SubCategory",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ItemSizeID",
                table: "ItemSize",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ItemCostHistoryID",
                table: "ItemCostHistory",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategory",
                table: "SubCategory",
                column: "SubCategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemSize",
                table: "ItemSize",
                column: "ItemSizeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemCostHistory",
                table: "ItemCostHistory",
                column: "ItemCostHistoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_GenderSubCategory_SubCategory_SubCategoryID",
                table: "GenderSubCategory",
                column: "SubCategoryID",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_ItemSize_ItemSizeID",
                table: "Inventory",
                column: "ItemSizeID",
                principalTable: "ItemSize",
                principalColumn: "ItemSizeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ItemSize_ItemSizeID",
                table: "Order",
                column: "ItemSizeID",
                principalTable: "ItemSize",
                principalColumn: "ItemSizeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_ItemSize_ItemSizeID",
                table: "Purchase",
                column: "ItemSizeID",
                principalTable: "ItemSize",
                principalColumn: "ItemSizeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_ItemSize_ItemSizeID",
                table: "Warehouse",
                column: "ItemSizeID",
                principalTable: "ItemSize",
                principalColumn: "ItemSizeID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
