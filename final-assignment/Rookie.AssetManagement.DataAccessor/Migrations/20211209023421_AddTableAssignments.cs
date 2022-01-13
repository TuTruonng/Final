using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rookie.AssetManagement.DataAccessor.Migrations
{
    public partial class AddTableAssignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    AssignedToUserId = table.Column<int>(type: "int", nullable: false),
                    AssignedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_Assignments_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "AssetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Users_AssignedByUserId",
                        column: x => x.AssignedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assignments_Users_AssignedToUserId",
                        column: x => x.AssignedToUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 1,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 2,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 3,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 4,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 5,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 6,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 7,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 8,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 9,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 10,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 11,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 12,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 13,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 14,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 15,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 16,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 17,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "AssignmentId", "AssetId", "AssignedByUserId", "AssignedDate", "AssignedToUserId", "Note", "State" },
                values: new object[,]
                {
                    { 9, 24, 1, new DateTime(2021, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, null, 0 },
                    { 8, 22, 1, new DateTime(2021, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, null, 0 },
                    { 7, 21, 1, new DateTime(2021, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, null, 1 },
                    { 6, 20, 1, new DateTime(2021, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, null, 0 },
                    { 5, 14, 1, new DateTime(2021, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 4, 13, 1, new DateTime(2021, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 3, 8, 1, new DateTime(2021, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 1 },
                    { 2, 7, 1, new DateTime(2021, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null, 1 },
                    { 10, 25, 1, new DateTime(2021, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, null, 0 },
                    { 1, 3, 4, new DateTime(2021, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssetId",
                table: "Assignments",
                column: "AssetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssignedByUserId",
                table: "Assignments",
                column: "AssignedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssignedToUserId",
                table: "Assignments",
                column: "AssignedToUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 1,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(2124));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 2,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7774));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 3,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7794));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 4,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7797));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 5,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7799));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 6,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7801));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 7,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7802));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 8,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7804));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 9,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7806));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 10,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7808));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 11,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7810));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 12,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7812));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 13,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7814));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 14,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7816));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 15,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7819));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 16,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7821));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 17,
                column: "InstalledDate",
                value: new DateTime(2021, 12, 4, 15, 58, 42, 655, DateTimeKind.Local).AddTicks(7895));
        }
    }
}
