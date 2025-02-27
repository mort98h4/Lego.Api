using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lego.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPendingChangesToContextModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NoOfParts",
                table: "Sets",
                newName: "NoOfPieces");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NoOfPieces",
                table: "Sets",
                newName: "NoOfParts");
        }
    }
}
