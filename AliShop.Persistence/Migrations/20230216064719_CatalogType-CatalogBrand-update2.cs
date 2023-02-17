using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AliShop.Persistence.Migrations
{
    public partial class CatalogTypeCatalogBrandupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsRemoved",
                table: "CatalogType",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 16, 9, 47, 19, 26, DateTimeKind.Local).AddTicks(487),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRemoved",
                table: "CatalogBrand",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 16, 9, 47, 19, 17, DateTimeKind.Local).AddTicks(7439),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "CatalogBrand",
                columns: new[] { "Id", "Brand", "RemovedTime", "UpdateTime" },
                values: new object[,]
                {
                    { 1, "سامسونگ", null, null },
                    { 2, "شیائومی ", null, null },
                    { 3, "اپل", null, null },
                    { 4, "هوآوی", null, null },
                    { 5, "نوکیا ", null, null },
                    { 6, "ال جی", null, null }
                });

            migrationBuilder.InsertData(
                table: "CatalogType",
                columns: new[] { "Id", "ParentCatalogNameId", "ParentCatalogTypeId", "RemovedTime", "Type", "UpdateTime" },
                values: new object[,]
                {
                    { 1, null, null, null, "کالای دیجیتال", null },
                    { 2, null, 1, null, "لوازم جانبی گوشی", null },
                    { 3, null, 2, null, "پایه نگهدارنده گوشی", null },
                    { 4, null, 2, null, "پاور بانک (شارژر همراه)", null },
                    { 5, null, 2, null, "کیف و کاور گوشی", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CatalogBrand",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CatalogBrand",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CatalogBrand",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CatalogBrand",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CatalogBrand",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CatalogBrand",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<bool>(
                name: "IsRemoved",
                table: "CatalogType",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 16, 9, 47, 19, 26, DateTimeKind.Local).AddTicks(487));

            migrationBuilder.AlterColumn<bool>(
                name: "IsRemoved",
                table: "CatalogBrand",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 16, 9, 47, 19, 17, DateTimeKind.Local).AddTicks(7439));
        }
    }
}
