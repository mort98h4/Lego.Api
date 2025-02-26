using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lego.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCollectionToSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Collections_SeriesId",
                table: "Sets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.RenameTable(
                name: "Collections",
                newName: "Series");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Series",
                table: "Series",
                column: "Id");

            migrationBuilder.RenameColumn(
                "CollectionId", 
                "Sets", 
                "SeriesId");

            migrationBuilder.UpdateData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 1,
                column: "SeriesId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 3,
                column: "SeriesId",
                value: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Series_SeriesId",
                table: "Sets",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Series_SeriesId",
                table: "Sets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Series",
                table: "Series");

            migrationBuilder.RenameTable(
                name: "Series",
                newName: "Collections");

            migrationBuilder.RenameColumn(
                "SeriesId", 
                "Sets", 
                "CollectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 1,
                column: "SeriesId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 3,
                column: "SeriesId",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Collections_SeriesId",
                table: "Sets",
                column: "SeriesId",
                principalTable: "Collections",
                principalColumn: "Id");
        }
    }
}
