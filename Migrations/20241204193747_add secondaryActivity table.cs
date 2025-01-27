using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xo_Test.Migrations
{
    /// <inheritdoc />
    public partial class addsecondaryActivitytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondaryActivities",
                table: "Business");

            migrationBuilder.CreateTable(
                name: "SecondaryActivity",
                columns: table => new
                {
                    SecondaryActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BusinessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryActivity", x => x.SecondaryActivityId);
                    table.ForeignKey(
                        name: "FK_SecondaryActivity_Business_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Business",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryActivity_BusinessId",
                table: "SecondaryActivity",
                column: "BusinessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecondaryActivity");

            migrationBuilder.AddColumn<string>(
                name: "SecondaryActivities",
                table: "Business",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
