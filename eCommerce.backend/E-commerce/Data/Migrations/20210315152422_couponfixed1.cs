using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class couponfixed1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "Coupon",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "Coupon",
                type: "double",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
