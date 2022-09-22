using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lobster.Adventures.Infrastructure.Migrations
{
    public partial class JourneySeedFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserJourneys",
                keyColumn: "Id",
                keyValue: new Guid("254d8f7a-bc27-4aff-bb4d-1b232b8de4a6"),
                columns: new[] { "DateTimeCreated", "DateTimeUpdated", "Path" },
                values: new object[] { new DateTime(2022, 9, 21, 22, 35, 21, 164, DateTimeKind.Utc).AddTicks(5258), new DateTime(2022, 9, 21, 22, 35, 21, 164, DateTimeKind.Utc).AddTicks(5259), ",209005df-5897-4491-992e-c25cd9aca290,0e4a446b-adc7-430d-8b84-5ffaca507682,f7aa73d6-5566-4278-8ae7-a8e273d944a8,096a086d-980b-44cb-bfa3-382d8f844ee2," });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e"),
                column: "CreatedDateTime",
                value: new DateTime(2022, 9, 21, 23, 35, 21, 164, DateTimeKind.Local).AddTicks(4936));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserJourneys",
                keyColumn: "Id",
                keyValue: new Guid("254d8f7a-bc27-4aff-bb4d-1b232b8de4a6"),
                columns: new[] { "DateTimeCreated", "DateTimeUpdated", "Path" },
                values: new object[] { new DateTime(2022, 9, 21, 13, 22, 47, 219, DateTimeKind.Utc).AddTicks(1855), new DateTime(2022, 9, 21, 13, 22, 47, 219, DateTimeKind.Utc).AddTicks(1856), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e"),
                column: "CreatedDateTime",
                value: new DateTime(2022, 9, 21, 14, 22, 47, 219, DateTimeKind.Local).AddTicks(1564));
        }
    }
}
