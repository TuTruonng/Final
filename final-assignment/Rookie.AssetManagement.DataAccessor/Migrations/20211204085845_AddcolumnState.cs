using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rookie.AssetManagement.DataAccessor.Migrations
{
    public partial class AddcolumnState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Assets",
                newName: "StateId");

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "Name" },
                values: new object[,]
                {
                    { 1, "Assigned" },
                    { 2, "Available" },
                    { 3, "Not available" },
                    { 4, "Waiting for recycling" },
                    { 5, "Recycled" }
                });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 1,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(2124), 2 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 2,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7774), 2 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 3,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7794), 1 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 4,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7797), 3 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 5,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7799), 2 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 6,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7801), 2 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 7,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7802), 1 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 8,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7804), 1 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 9,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7806), 2 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 10,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7808), 2 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 11,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7810), 2 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 12,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7812), 3 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 13,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7814), 1 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 14,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7816), 1 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 15,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7819), 2 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 16,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7821), 3 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 17,
                columns: new[] { "InstalledDate", "StateId" },
                values: new object[] { new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7895), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_StateId",
                table: "Assets",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_States_StateId",
                table: "Assets",
                column: "StateId",
                principalTable: "States",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_States_StateId",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropIndex(
                name: "IX_Assets_StateId",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "Assets",
                newName: "State");

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 1,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 762, DateTimeKind.Local).AddTicks(4813), 0 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 2,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(1975), 0 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 3,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(1995), 2 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 4,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(1997), 1 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 5,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(1999), 0 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 6,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2000), 0 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 7,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2001), 2 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 8,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2003), 2 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 9,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2004), 0 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 10,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2005), 0 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 11,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2006), 0 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 12,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2008), 1 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 13,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2009), 2 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 14,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2011), 2 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 15,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2012), 0 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 16,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2014), 1 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 17,
                columns: new[] { "InstalledDate", "State" },
                values: new object[] { new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2015), 2 });
        }
    }
}
