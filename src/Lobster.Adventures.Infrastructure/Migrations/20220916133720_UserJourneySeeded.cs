using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lobster.Adventures.Infrastructure.Migrations
{
    public partial class UserJourneySeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "UserJourneys",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Adventures",
                keyColumn: "Id",
                keyValue: new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                columns: new[] { "Depth", "NumberOfNodes", "RootNodeId" },
                values: new object[] { 0, 0, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDateTime", "Email", "FirstName", "LastName" },
                values: new object[] { new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e"), new DateTime(2022, 9, 16, 14, 37, 19, 608, DateTimeKind.Local).AddTicks(7017), "johnsmith@email.com", "John", "Smith" });

            migrationBuilder.InsertData(
                table: "UserJourneys",
                columns: new[] { "Id", "AdventureId", "DateTimeCreated", "DateTimeUpdated", "Path", "Status", "UserId" },
                values: new object[] { new Guid("254d8f7a-bc27-4aff-bb4d-1b232b8de4a6"), new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), new DateTime(2022, 9, 16, 13, 37, 19, 608, DateTimeKind.Utc).AddTicks(7382), new DateTime(2022, 9, 16, 13, 37, 19, 608, DateTimeKind.Utc).AddTicks(7383), null, 0, new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserJourneys",
                keyColumn: "Id",
                keyValue: new Guid("254d8f7a-bc27-4aff-bb4d-1b232b8de4a6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e"));

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "UserJourneys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Adventures",
                keyColumn: "Id",
                keyValue: new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"),
                columns: new[] { "Depth", "NumberOfNodes", "RootNodeId" },
                values: new object[] { 4, 9, new Guid("209005df-5897-4491-992e-c25cd9aca290") });
        }
    }
}