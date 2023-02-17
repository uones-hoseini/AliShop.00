using Microsoft.EntityFrameworkCore.Migrations;

namespace AliShop.Persistence.Migrations
{
    public partial class CatalogTypeCatalogBrandupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentCatalogNameId",
                table: "CatalogType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentCatalogTypeId",
                table: "CatalogType",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CatalogType_ParentCatalogNameId",
                table: "CatalogType",
                column: "ParentCatalogNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogType_CatalogType_ParentCatalogNameId",
                table: "CatalogType",
                column: "ParentCatalogNameId",
                principalTable: "CatalogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogType_CatalogType_ParentCatalogNameId",
                table: "CatalogType");

            migrationBuilder.DropIndex(
                name: "IX_CatalogType_ParentCatalogNameId",
                table: "CatalogType");

            migrationBuilder.DropColumn(
                name: "ParentCatalogNameId",
                table: "CatalogType");

            migrationBuilder.DropColumn(
                name: "ParentCatalogTypeId",
                table: "CatalogType");
        }
    }
}
