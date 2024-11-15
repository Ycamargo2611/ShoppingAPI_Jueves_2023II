using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingAPI_Jueves_2023II.Migrations
{
    public partial class StateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Prductos_ProductosID",
                        column: x => x.ProductosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_States_ProductosId",
                table: "States",
                column: "ProductosId");

            migrationBuilder.CreateIndex(
                name: "IX_States_Name_ProductosId",
                table: "States",
                columns: new[] { "Name", "ProductosId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
