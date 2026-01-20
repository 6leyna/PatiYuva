using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PatiYuva.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberToAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "009c15b7-4788-41fa-aba2-5b141c742937");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7987af6c-5b36-4063-a0e4-890584619a77");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Animals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e9f6868-bb71-463a-bfc4-909ad07c63cb", null, "Sahip", "SAHIP" },
                    { "5020fc54-442d-4a12-914a-a1338d859bd2", null, "Sahiplenici", "SAHIPLENCILI" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e9f6868-bb71-463a-bfc4-909ad07c63cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5020fc54-442d-4a12-914a-a1338d859bd2");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Animals");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "009c15b7-4788-41fa-aba2-5b141c742937", null, "Sahiplenici", "SAHIPLENCILI" },
                    { "7987af6c-5b36-4063-a0e4-890584619a77", null, "Sahip", "SAHIP" }
                });
        }
    }
}
