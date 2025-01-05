using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseDonor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserCity_Table_Change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "UserCity",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "UserCity");
        }
    }
}
