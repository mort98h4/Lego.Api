using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lego.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Small starships made for displaying.", "The Mid-Scale Starship Collection" },
                    { 2, "Large sets made with the utmost care and focus on details.", "Ultimate Collector's Series" }
                });

            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "One of the world's most popular franchises.", "Star Wars" },
                    { 2, "Go on magical adventures with Harry, Ron, and Hermione.", "Harry Potter" }
                });

            migrationBuilder.InsertData(
                table: "Sets",
                columns: new[] { "Id", "CollectionId", "Description", "HasBox", "IsSealed", "ModelNo", "Name", "NoOfParts", "ThemeId" },
                values: new object[,]
                {
                    { 1, 1, "A display piece of a Republic Era assault ship.", 1, 1, "75404", "Acclamator-Class Assault Ship", 450, 1 },
                    { 2, null, "The first edition of Hagrid's Hut.", 0, 0, "4707", "Hagrid's Hut", 288, 2 },
                    { 3, 2, "This is the AT-AT that alle Lego Star Wars collectors have been waiting for.", 1, 0, "75313", "AT-AT", 6785, 1 }
                });

            migrationBuilder.InsertData(
                table: "Parts",
                columns: new[] { "Id", "Color", "Description", "PartNo", "Quantity", "SetId" },
                values: new object[] { 1, "Brown", "Fence 1 x 4 x 2 Spindled with 2 Studs", "30055", 1, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
