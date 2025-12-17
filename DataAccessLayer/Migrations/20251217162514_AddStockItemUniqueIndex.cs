using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddStockItemUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockItems_StockUnitId",
                table: "StockItems");

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000001"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5096));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000002"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5098));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000003"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5100));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000004"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5101));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000005"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5103));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000006"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5104));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000007"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5106));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000008"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5107));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000009"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5109));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-00000000000a"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5110));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(4943));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(4946));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(4947));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(4947));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(4948));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(4949));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(4949));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(4950));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(4951));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000001"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5059));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000002"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5065));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000003"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5067));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000004"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5068));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000005"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5071));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000006"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5073));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000007"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5074));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000008"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5076));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000009"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5077));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-00000000000a"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 16, 25, 14, 650, DateTimeKind.Utc).AddTicks(5079));

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_StockUnitId",
                table: "StockItems",
                column: "StockUnitId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockItems_StockUnitId",
                table: "StockItems");

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000001"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3772));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000002"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3774));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000003"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3775));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000004"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3777));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000005"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3778));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000006"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000007"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3781));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000008"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3782));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000009"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3784));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-00000000000a"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3785));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3600));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3604));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3604));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3605));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3606));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3609));

            migrationBuilder.UpdateData(
                table: "StockTypes",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3609));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000001"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3733));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000002"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3739));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000003"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3742));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000004"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3743));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000005"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3746));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000006"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3747));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000007"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3749));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000008"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3750));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000009"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3752));

            migrationBuilder.UpdateData(
                table: "StockUnits",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-00000000000a"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3753));

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_StockUnitId",
                table: "StockItems",
                column: "StockUnitId");
        }
    }
}
