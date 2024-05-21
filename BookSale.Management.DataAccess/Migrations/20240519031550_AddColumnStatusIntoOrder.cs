using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSale.Management.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnStatusIntoOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 19, 10, 15, 48, 860, DateTimeKind.Local).AddTicks(3255));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 19, 10, 15, 48, 860, DateTimeKind.Local).AddTicks(3271));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 19, 10, 15, 48, 860, DateTimeKind.Local).AddTicks(3274));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 19, 10, 15, 48, 860, DateTimeKind.Local).AddTicks(3276));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 19, 10, 15, 48, 860, DateTimeKind.Local).AddTicks(3280));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 19, 10, 15, 48, 860, DateTimeKind.Local).AddTicks(3283));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 19, 10, 15, 48, 860, DateTimeKind.Local).AddTicks(3285));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 19, 10, 15, 48, 860, DateTimeKind.Local).AddTicks(3288));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 19, 10, 15, 48, 860, DateTimeKind.Local).AddTicks(3290));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 19, 10, 15, 48, 860, DateTimeKind.Local).AddTicks(3293));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 19, 10, 15, 48, 860, DateTimeKind.Local).AddTicks(3295));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 19, 10, 15, 48, 860, DateTimeKind.Local).AddTicks(3300));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2024, 5, 19, 10, 15, 48, 860, DateTimeKind.Local).AddTicks(3304));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 15, 21, 17, 22, 811, DateTimeKind.Local).AddTicks(4613));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 15, 21, 17, 22, 811, DateTimeKind.Local).AddTicks(4633));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 15, 21, 17, 22, 811, DateTimeKind.Local).AddTicks(4636));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 15, 21, 17, 22, 811, DateTimeKind.Local).AddTicks(4639));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 15, 21, 17, 22, 811, DateTimeKind.Local).AddTicks(4642));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 15, 21, 17, 22, 811, DateTimeKind.Local).AddTicks(4645));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 15, 21, 17, 22, 811, DateTimeKind.Local).AddTicks(4647));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 15, 21, 17, 22, 811, DateTimeKind.Local).AddTicks(4650));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 15, 21, 17, 22, 811, DateTimeKind.Local).AddTicks(4652));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 15, 21, 17, 22, 811, DateTimeKind.Local).AddTicks(4655));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 15, 21, 17, 22, 811, DateTimeKind.Local).AddTicks(4657));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 15, 21, 17, 22, 811, DateTimeKind.Local).AddTicks(4660));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 15, 21, 17, 22, 811, DateTimeKind.Local).AddTicks(4663));
        }
    }
}
