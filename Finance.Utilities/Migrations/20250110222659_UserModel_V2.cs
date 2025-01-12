using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance.Utilities.Migrations
{
    /// <inheritdoc />
    public partial class UserModel_V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "users",
                newName: "Name");
        }
    }
}
