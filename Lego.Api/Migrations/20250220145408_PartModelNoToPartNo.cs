using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lego.Api.Migrations
{
    /// <inheritdoc />
    public partial class PartModelNoToPartNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModelNo",
                table: "Parts",
                newName: "PartNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartNo",
                table: "Parts",
                newName: "ModelNo");
        }
    }
}
