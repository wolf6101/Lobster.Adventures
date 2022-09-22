using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lobster.Adventures.Infrastructure.Migrations
{
    public partial class FixingDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Adventures",
                keyColumn: "Id",
                keyValue: new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                columns: new[] { "NumberOfNodes", "RootNodeId" },
                values: new object[] { 9, new Guid("209005df-5897-4491-992e-c25cd9aca290") });

            migrationBuilder.UpdateData(
                table: "UserJourneys",
                keyColumn: "Id",
                keyValue: new Guid("254d8f7a-bc27-4aff-bb4d-1b232b8de4a6"),
                columns: new[] { "DateTimeCreated", "DateTimeUpdated" },
                values: new object[] { new DateTime(2022, 9, 21, 13, 22, 47, 219, DateTimeKind.Utc).AddTicks(1855), new DateTime(2022, 9, 21, 13, 22, 47, 219, DateTimeKind.Utc).AddTicks(1856) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e"),
                column: "CreatedDateTime",
                value: new DateTime(2022, 9, 21, 14, 22, 47, 219, DateTimeKind.Local).AddTicks(1564));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Adventures",
                keyColumn: "Id",
                keyValue: new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                columns: new[] { "NumberOfNodes", "RootNodeId" },
                values: new object[] { 0, new Guid("00000000-0000-0000-0000-000000000000") });

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
    }
}