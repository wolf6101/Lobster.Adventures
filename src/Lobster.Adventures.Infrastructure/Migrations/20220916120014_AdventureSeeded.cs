using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lobster.Adventures.Infrastructure.Migrations
{
    public partial class AdventureSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserJourneys_Adventures_AdventureId",
                table: "UserJourneys");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Adventures",
                newName: "NumberOfNodes");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Adventures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AdventureNode",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Adventures",
                columns: new[] { "Id", "Depth", "Description", "Name", "NumberOfNodes", "RootNodeId" },
                values: new object[] { new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), 3, "Adventure", "Doughnut adventure", 9, new Guid("209005df-5897-4491-992e-c25cd9aca290") });

            migrationBuilder.InsertData(
                table: "AdventureNode",
                columns: new[] { "Id", "AdventureId", "Description", "LeftChildId", "Name", "ParentId", "RightChildId" },
                values: new object[,]
                {
                    { new Guid("096a086d-980b-44cb-bfa3-382d8f844ee2"), new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), null, null, "Get it!", new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8"), null },
                    { new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682"), new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), null, new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8"), "Do I deserve it?", new Guid("209005df-5897-4491-992e-c25cd9aca290"), new Guid("130454c8-4a63-40b3-b400-b2b13dc34809") },
                    { new Guid("130454c8-4a63-40b3-b400-b2b13dc34809"), new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), null, new Guid("4ae2afff-92e8-4ac3-b934-3d07be023f3d"), "Is it a good doughnut?", new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682"), new Guid("2f6a6663-90f8-4313-8441-dda39df5d677") },
                    { new Guid("209005df-5897-4491-992e-c25cd9aca290"), new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), null, new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682"), "Do I want Doughnut?", null, new Guid("f287a87e-148c-4ffe-aed7-37bb6baefbb8") },
                    { new Guid("2f6a6663-90f8-4313-8441-dda39df5d677"), new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), null, null, "Wait for a better one!", new Guid("130454c8-4a63-40b3-b400-b2b13dc34809"), null },
                    { new Guid("4ae2afff-92e8-4ac3-b934-3d07be023f3d"), new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), null, null, "Grab it now!", new Guid("130454c8-4a63-40b3-b400-b2b13dc34809"), null },
                    { new Guid("95069404-d73c-4ba9-8a1e-5f76bb51e790"), new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), null, null, "Do jumping jacks first!", new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8"), null },
                    { new Guid("f287a87e-148c-4ffe-aed7-37bb6baefbb8"), new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), null, null, "Maybe you want an apple?", new Guid("209005df-5897-4491-992e-c25cd9aca290"), null },
                    { new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8"), new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"), null, new Guid("096a086d-980b-44cb-bfa3-382d8f844ee2"), "Are you sure?", new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682"), new Guid("95069404-d73c-4ba9-8a1e-5f76bb51e790") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserJourneys_Adventures_AdventureId",
                table: "UserJourneys",
                column: "AdventureId",
                principalTable: "Adventures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserJourneys_Adventures_AdventureId",
                table: "UserJourneys");

            migrationBuilder.DeleteData(
                table: "AdventureNode",
                keyColumn: "Id",
                keyValue: new Guid("096a086d-980b-44cb-bfa3-382d8f844ee2"));

            migrationBuilder.DeleteData(
                table: "AdventureNode",
                keyColumn: "Id",
                keyValue: new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682"));

            migrationBuilder.DeleteData(
                table: "AdventureNode",
                keyColumn: "Id",
                keyValue: new Guid("130454c8-4a63-40b3-b400-b2b13dc34809"));

            migrationBuilder.DeleteData(
                table: "AdventureNode",
                keyColumn: "Id",
                keyValue: new Guid("209005df-5897-4491-992e-c25cd9aca290"));

            migrationBuilder.DeleteData(
                table: "AdventureNode",
                keyColumn: "Id",
                keyValue: new Guid("2f6a6663-90f8-4313-8441-dda39df5d677"));

            migrationBuilder.DeleteData(
                table: "AdventureNode",
                keyColumn: "Id",
                keyValue: new Guid("4ae2afff-92e8-4ac3-b934-3d07be023f3d"));

            migrationBuilder.DeleteData(
                table: "AdventureNode",
                keyColumn: "Id",
                keyValue: new Guid("95069404-d73c-4ba9-8a1e-5f76bb51e790"));

            migrationBuilder.DeleteData(
                table: "AdventureNode",
                keyColumn: "Id",
                keyValue: new Guid("f287a87e-148c-4ffe-aed7-37bb6baefbb8"));

            migrationBuilder.DeleteData(
                table: "AdventureNode",
                keyColumn: "Id",
                keyValue: new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8"));

            migrationBuilder.DeleteData(
                table: "Adventures",
                keyColumn: "Id",
                keyValue: new Guid("35168b83-b5f4-4079-b674-12b5f32e995e"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Adventures");

            migrationBuilder.RenameColumn(
                name: "NumberOfNodes",
                table: "Adventures",
                newName: "Size");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AdventureNode",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserJourneys_Adventures_AdventureId",
                table: "UserJourneys",
                column: "AdventureId",
                principalTable: "Adventures",
                principalColumn: "Id");
        }
    }
}