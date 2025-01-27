using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xo_Test.Migrations
{
    /// <inheritdoc />
    public partial class convertingfromstringtointthephonenumberfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Διαγραφή μη έγκυρων δεδομένων
            migrationBuilder.Sql("DELETE FROM Phone WHERE TRY_CAST(Number AS int) IS NULL");

            // Μετατροπή της στήλης Number σε int
            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Phone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Phone",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
