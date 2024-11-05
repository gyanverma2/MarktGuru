using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MarktGuru.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseInitalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsAvailable = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 11, 4, 19, 33, 44, 64, DateTimeKind.Utc).AddTicks(37)),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SourceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    AmountExclTax = table.Column<decimal>(type: "TEXT", nullable: false),
                    TaxPercentage = table.Column<decimal>(type: "TEXT", nullable: false),
                    SourceTypeId = table.Column<int>(type: "smallint", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 11, 4, 19, 33, 44, 64, DateTimeKind.Utc).AddTicks(937)),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsApproved = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 11, 4, 19, 33, 44, 64, DateTimeKind.Utc).AddTicks(1424)),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.Id);
                    table.CheckConstraint("CK_Price_BeginDate_EndDate", "EndDate IS NULL OR BeginDate < EndDate");
                    table.ForeignKey(
                        name: "FK_ProductPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SourceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Any" },
                    { 2, "Web" },
                    { 3, "Mobile" },
                    { 4, "Tablet" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId_BeginDate_SourceTypeId",
                table: "ProductPrices",
                columns: new[] { "ProductId", "BeginDate", "SourceTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SourceTypes_Name",
                table: "SourceTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "SourceTypes");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
