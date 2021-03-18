using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class checkupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cvc",
                table: "CreditCard",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "Check",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Check",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Check",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cvc",
                table: "CreditCard");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Check");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Check");

            migrationBuilder.AlterColumn<float>(
                name: "Discount",
                table: "Check",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
