using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lego.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddFKToPartEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartSet");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Parts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SetId",
                table: "Parts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_SetId",
                table: "Parts",
                column: "SetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Sets_SetId",
                table: "Parts",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Sets_SetId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_SetId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "SetId",
                table: "Parts");

            migrationBuilder.CreateTable(
                name: "PartSet",
                columns: table => new
                {
                    MissingPartsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SetsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartSet", x => new { x.MissingPartsId, x.SetsId });
                    table.ForeignKey(
                        name: "FK_PartSet_Parts_MissingPartsId",
                        column: x => x.MissingPartsId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartSet_Sets_SetsId",
                        column: x => x.SetsId,
                        principalTable: "Sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartSet_SetsId",
                table: "PartSet",
                column: "SetsId");
        }
    }
}
