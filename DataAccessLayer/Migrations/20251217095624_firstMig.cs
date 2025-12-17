using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class firstMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueryString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    LogLevel = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrelationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DurationMs = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityUnit = table.Column<int>(type: "int", nullable: false),
                    BuyingCurrency = table.Column<int>(type: "int", nullable: false),
                    SellingCurrency = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaperWeight = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockUnits_StockTypes_StockTypeId",
                        column: x => x.StockTypeId,
                        principalTable: "StockTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Shelf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cabinet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriticalQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockItems_StockUnits_StockUnitId",
                        column: x => x.StockUnitId,
                        principalTable: "StockUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StockTypes",
                columns: new[] { "Id", "CreatedDate", "IsActive", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3600), true, "Kağıt", null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3604), true, "Zarf", null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3604), true, "Zımba", null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3605), true, "Toner", null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3606), true, "Bant", null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3607), true, "Karton", null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3607), true, "Folyo", null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3609), true, "Forex", null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3609), true, "Bitmiş Ürün", null }
                });

            migrationBuilder.InsertData(
                table: "StockUnits",
                columns: new[] { "Id", "BuyingCurrency", "BuyingPrice", "Code", "CreatedDate", "Description", "IsActive", "PaperWeight", "QuantityUnit", "SellingCurrency", "SellingPrice", "StockTypeId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000001"), 0, 0.00m, "8544545", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3733), null, true, null, 4, 0, 0.00m, new Guid("55555555-5555-5555-5555-555555555555"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000002"), 0, 400.00m, "2434536436", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3739), "Jnyfyv", true, 100, 1, 0, 700.00m, new Guid("33333333-3333-3333-3333-333333333333"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000003"), 0, 0.00m, "12345678906", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3742), "TEST", true, 0, 0, 0, 0.00m, new Guid("11111111-1111-1111-1111-111111111111"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000004"), 0, 0m, "744953400", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3743), "744953400E", true, 0, 0, 0, 0m, new Guid("11111111-1111-1111-1111-111111111111"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000005"), 0, 0m, "747223200", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3746), "747223200E", true, 0, 0, 0, 0m, new Guid("11111111-1111-1111-1111-111111111111"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000006"), 0, 0m, "748273300", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3747), "748273300E", true, 0, 0, 0, 0m, new Guid("11111111-1111-1111-1111-111111111111"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000007"), 0, 0m, "750221650", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3749), "750221650E", true, 0, 0, 0, 0m, new Guid("11111111-1111-1111-1111-111111111111"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000008"), 0, 0m, "750223600", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3750), "750223600E", true, 0, 0, 0, 0m, new Guid("11111111-1111-1111-1111-111111111111"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000009"), 0, 0m, "7248273300", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3752), "7248273300E", true, 0, 0, 0, 0m, new Guid("11111111-1111-1111-1111-111111111111"), null },
                    { new Guid("aaaaaaaa-0000-0000-0000-00000000000a"), 0, 0m, "7250222500", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3753), "7250222500E", true, 0, 0, 0, 0m, new Guid("11111111-1111-1111-1111-111111111111"), null }
                });

            migrationBuilder.InsertData(
                table: "StockItems",
                columns: new[] { "Id", "Cabinet", "CreatedDate", "CriticalQuantity", "IsActive", "Quantity", "Shelf", "StockClass", "StockUnitId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("bbbbbbbb-0000-0000-0000-000000000001"), "D-01", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3772), 100.00m, true, 1232257.99m, "A-01", "RawMaterial", new Guid("aaaaaaaa-0000-0000-0000-000000000003"), null },
                    { new Guid("bbbbbbbb-0000-0000-0000-000000000002"), "D-01", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3774), 0.00m, true, 500.00m, "A-02", "RawMaterial", new Guid("aaaaaaaa-0000-0000-0000-000000000004"), null },
                    { new Guid("bbbbbbbb-0000-0000-0000-000000000003"), "D-02", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3775), 0.00m, true, 508.00m, "A-02", "RawMaterial", new Guid("aaaaaaaa-0000-0000-0000-000000000005"), null },
                    { new Guid("bbbbbbbb-0000-0000-0000-000000000004"), "D-02", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3777), 0.00m, true, 15.00m, "B-01", "RawMaterial", new Guid("aaaaaaaa-0000-0000-0000-000000000006"), null },
                    { new Guid("bbbbbbbb-0000-0000-0000-000000000005"), "D-03", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3778), 0.00m, true, 49.00m, "B-01", "RawMaterial", new Guid("aaaaaaaa-0000-0000-0000-000000000007"), null },
                    { new Guid("bbbbbbbb-0000-0000-0000-000000000006"), "D-03", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3780), 20.00m, true, 104891966.00m, "B-02", "RawMaterial", new Guid("aaaaaaaa-0000-0000-0000-000000000008"), null },
                    { new Guid("bbbbbbbb-0000-0000-0000-000000000007"), "D-03", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3781), 1000.00m, true, 10000.00m, "C-01", "RawMaterial", new Guid("aaaaaaaa-0000-0000-0000-000000000009"), null },
                    { new Guid("bbbbbbbb-0000-0000-0000-000000000008"), "D-04", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3782), 5.00m, true, 10.00m, "C-01", "RawMaterial", new Guid("aaaaaaaa-0000-0000-0000-00000000000a"), null },
                    { new Guid("bbbbbbbb-0000-0000-0000-000000000009"), "D-04", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3784), 1.00m, true, 124719.00m, "C-02", "RawMaterial", new Guid("aaaaaaaa-0000-0000-0000-000000000001"), null },
                    { new Guid("bbbbbbbb-0000-0000-0000-00000000000a"), "D-05", new DateTime(2025, 12, 17, 9, 56, 24, 642, DateTimeKind.Utc).AddTicks(3785), 0.00m, true, 5451.00m, "D-01", "RawMaterial", new Guid("aaaaaaaa-0000-0000-0000-000000000002"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestLogs_CorrelationId",
                table: "RequestLogs",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestLogs_CreatedDate",
                table: "RequestLogs",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_RequestLogs_StatusCode",
                table: "RequestLogs",
                column: "StatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_StockUnitId",
                table: "StockItems",
                column: "StockUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTypes_Name",
                table: "StockTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockUnits_Code",
                table: "StockUnits",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockUnits_StockTypeId",
                table: "StockUnits",
                column: "StockTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestLogs");

            migrationBuilder.DropTable(
                name: "StockItems");

            migrationBuilder.DropTable(
                name: "StockUnits");

            migrationBuilder.DropTable(
                name: "StockTypes");
        }
    }
}
