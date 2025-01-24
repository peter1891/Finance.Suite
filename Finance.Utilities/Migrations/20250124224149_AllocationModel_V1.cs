using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance.Utilities.Migrations
{
    /// <inheritdoc />
    public partial class AllocationModel_V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllocationId",
                table: "transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "allocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    TargetAmount = table.Column<double>(type: "REAL", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_allocations_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_allocations_AccountId",
                table: "allocations",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_allocations_AccountId",
                table: "transactions",
                column: "AccountId",
                principalTable: "allocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_allocations_AccountId",
                table: "transactions");

            migrationBuilder.DropTable(
                name: "allocations");

            migrationBuilder.DropColumn(
                name: "AllocationId",
                table: "transactions");
        }
    }
}
