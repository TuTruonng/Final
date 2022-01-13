using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rookie.AssetManagement.DataAccessor.Migrations
{
    public partial class AddTableCategoiesAndAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstalledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    History = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisable = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetId);
                    table.ForeignKey(
                        name: "FK_Assets_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name", "Prefix" },
                values: new object[] { 1, "Laptop", "LA" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name", "Prefix" },
                values: new object[] { 2, "Monitor", "MO" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name", "Prefix" },
                values: new object[] { 3, "Personal Computer", "PC" });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "AssetId", "AssetName", "CategoryId", "History", "InstalledDate", "Location", "Specification", "State" },
                values: new object[,]
                {
                    { 1, "Laptop HP Pro Book 450 G1", 1, null, new DateTime(2021, 12, 3, 13, 31, 30, 762, DateTimeKind.Local).AddTicks(4813), "HCM", "Core i5, 8GB RAM, 750 GB HDD, Windows 8", 0 },
                    { 13, "Personal Computer", 3, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2009), "HCM", null, 2 },
                    { 12, "Personal Computer", 3, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2008), "HCM", null, 1 },
                    { 11, "Personal Computer", 3, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2006), "HCM", null, 0 },
                    { 10, "Personal Computer", 3, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2005), "HCM", null, 0 },
                    { 16, "Monitor Dell UltraSharp", 2, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2014), "HN", null, 1 },
                    { 9, "Monitor Dell UltraSharp", 2, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2004), "HCM", null, 0 },
                    { 14, "Personal Computer", 3, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2011), "HCM", null, 2 },
                    { 8, "Monitor Dell UltraSharp", 2, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2003), "HCM", null, 2 },
                    { 6, "Monitor Dell UltraSharp", 2, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2000), "HCM", null, 0 },
                    { 5, "Monitor Dell UltraSharp", 2, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(1999), "HCM", null, 0 },
                    { 15, "Laptop HP Pro Book 450 G1", 1, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2012), "HN", null, 0 },
                    { 4, "Laptop HP Pro Book 450 G1", 1, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(1997), "HCM", "Core i5, 8GB RAM, 750 GB HDD, Windows 8", 1 },
                    { 3, "Laptop HP Pro Book 450 G1", 1, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(1995), "HCM", "Core i5, 8GB RAM, 750 GB HDD, Windows 8", 2 },
                    { 2, "Laptop HP Pro Book 450 G1", 1, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(1975), "HCM", "Core i5, 8GB RAM, 750 GB HDD, Windows 8", 0 },
                    { 7, "Monitor Dell UltraSharp", 2, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2001), "HCM", null, 2 },
                    { 17, "Personal Computer", 3, null, new DateTime(2021, 12, 3, 13, 31, 30, 763, DateTimeKind.Local).AddTicks(2015), "HN", null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_CategoryId",
                table: "Assets",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
