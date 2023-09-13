using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace banking_api.Migrations
{
    /// <inheritdoc />
    public partial class Addedtransactionamounttotransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TransactionAmount",
                table: "Transactions",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionAmount",
                table: "Transactions");
        }
    }
}
