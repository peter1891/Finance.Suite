using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance.Utilities.Migrations
{
    /// <inheritdoc />
    public partial class AccountModel_V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bank",
                table: "accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bank",
                table: "accounts");
        }
    }
}
