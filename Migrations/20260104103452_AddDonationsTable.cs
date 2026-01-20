using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PatiYuva.Migrations
{
    /// <inheritdoc />
    public partial class AddDonationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c762f87-80d2-429a-80c2-2896aa3582eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2de5513e-6476-448a-a80f-9a37e97e9477");

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    AnimalId = table.Column<int>(type: "INTEGER", nullable: false),
                    DonorName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DonorEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    DonorPhone = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    BankName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Iban = table.Column<string>(type: "TEXT", maxLength: 34, nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    DonationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Donations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "009c15b7-4788-41fa-aba2-5b141c742937", null, "Sahiplenici", "SAHIPLENCILI" },
                    { "7987af6c-5b36-4063-a0e4-890584619a77", null, "Sahip", "SAHIP" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_AnimalId",
                table: "Donations",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_UserId",
                table: "Donations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "009c15b7-4788-41fa-aba2-5b141c742937");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7987af6c-5b36-4063-a0e4-890584619a77");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c762f87-80d2-429a-80c2-2896aa3582eb", null, "Sahip", "SAHIP" },
                    { "2de5513e-6476-448a-a80f-9a37e97e9477", null, "Sahiplenici", "SAHIPLENCILI" }
                });
        }
    }
}
