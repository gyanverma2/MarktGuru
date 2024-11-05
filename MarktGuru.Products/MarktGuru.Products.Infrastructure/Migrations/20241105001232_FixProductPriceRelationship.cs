using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarktGuru.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixProductPriceRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 5, 0, 12, 32, 573, DateTimeKind.Utc).AddTicks(4254),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 11, 4, 19, 33, 44, 64, DateTimeKind.Utc).AddTicks(37));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductPrices",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 5, 0, 12, 32, 573, DateTimeKind.Utc).AddTicks(6010),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 11, 4, 19, 33, 44, 64, DateTimeKind.Utc).AddTicks(1424));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BeginDate",
                table: "ProductPrices",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 5, 0, 12, 32, 573, DateTimeKind.Utc).AddTicks(5555),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 11, 4, 19, 33, 44, 64, DateTimeKind.Utc).AddTicks(937));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 4, 19, 33, 44, 64, DateTimeKind.Utc).AddTicks(37),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 11, 5, 0, 12, 32, 573, DateTimeKind.Utc).AddTicks(4254));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductPrices",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 4, 19, 33, 44, 64, DateTimeKind.Utc).AddTicks(1424),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 11, 5, 0, 12, 32, 573, DateTimeKind.Utc).AddTicks(6010));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BeginDate",
                table: "ProductPrices",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 4, 19, 33, 44, 64, DateTimeKind.Utc).AddTicks(937),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 11, 5, 0, 12, 32, 573, DateTimeKind.Utc).AddTicks(5555));
        }
    }
}
