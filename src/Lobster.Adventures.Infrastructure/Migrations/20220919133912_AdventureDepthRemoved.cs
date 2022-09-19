using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lobster.Adventures.Infrastructure.Migrations
{
    public partial class AdventureDepthRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Depth",
                table: "Adventures");

            migrationBuilder.UpdateData(
                table: "UserJourneys",
                keyColumn: "Id",
                keyValue: new Guid("254d8f7a-bc27-4aff-bb4d-1b232b8de4a6"),
                columns: new[] { "DateTimeCreated", "DateTimeUpdated" },
                values: new object[] { new DateTime(2022, 9, 19, 13, 39, 11, 493, DateTimeKind.Utc).AddTicks(2751), new DateTime(2022, 9, 19, 13, 39, 11, 493, DateTimeKind.Utc).AddTicks(2752) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e"),
                column: "CreatedDateTime",
                value: new DateTime(2022, 9, 19, 14, 39, 11, 493, DateTimeKind.Local).AddTicks(2489));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Depth",
                table: "Adventures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "UserJourneys",
                keyColumn: "Id",
                keyValue: new Guid("254d8f7a-bc27-4aff-bb4d-1b232b8de4a6"),
                columns: new[] { "DateTimeCreated", "DateTimeUpdated" },
                values: new object[] { new DateTime(2022, 9, 16, 13, 37, 19, 608, DateTimeKind.Utc).AddTicks(7382), new DateTime(2022, 9, 16, 13, 37, 19, 608, DateTimeKind.Utc).AddTicks(7383) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e"),
                column: "CreatedDateTime",
                value: new DateTime(2022, 9, 16, 14, 37, 19, 608, DateTimeKind.Local).AddTicks(7017));
        }
    }
}
