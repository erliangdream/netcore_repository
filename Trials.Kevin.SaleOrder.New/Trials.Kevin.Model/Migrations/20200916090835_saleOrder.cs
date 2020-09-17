using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Trials.Kevin.Model.Migrations
{
    public partial class saleOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kevin.SaleOrder.Order",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OrderNo = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SignDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(2000)", nullable: true),
                    CreateUserNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateUserNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kevin.SaleOrder.Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kevin.SaleOrder.OrderDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PId = table.Column<long>(nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    ProjectNo = table.Column<int>(nullable: false),
                    MaterialNo = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Num = table.Column<double>(nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    SortNo = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(2000)", nullable: true),
                    CreateUserNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateUserNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kevin.SaleOrder.OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kevin.SaleOrder.OrderDetail_Kevin.SaleOrder.Order_PId",
                        column: x => x.PId,
                        principalTable: "Kevin.SaleOrder.Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kevin.SaleOrder.OrderDetail_PId",
                table: "Kevin.SaleOrder.OrderDetail",
                column: "PId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kevin.SaleOrder.OrderDetail");

            migrationBuilder.DropTable(
                name: "Kevin.SaleOrder.Order");
        }
    }
}
