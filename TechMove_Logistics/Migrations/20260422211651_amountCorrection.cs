using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMove_Logistics.Migrations
{
    /// <inheritdoc />
    public partial class amountCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Payments",
                newName: "AmountZAR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountZAR",
                table: "Payments",
                newName: "Amount");
        }
    }
}
