using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xo_Test.Migrations
{
    /// <inheritdoc />
    public partial class addisActivepropertytobusinesstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Business",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Business");
        }
    }
}
