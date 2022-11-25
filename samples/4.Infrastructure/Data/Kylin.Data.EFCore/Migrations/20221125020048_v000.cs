using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kylin.Data.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class v000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false, comment: "消费次数"),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, comment: "消费总额"),
                    incount = table.Column<int>(name: "in_count", type: "int", nullable: false, comment: "入场次数"),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "tinyint(1)", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false),
                    creatorid = table.Column<long>(name: "creator_id", type: "bigint", nullable: false),
                    creator = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reviserid = table.Column<long>(name: "reviser_id", type: "bigint", nullable: false),
                    reviser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    revisetime = table.Column<DateTime>(name: "revise_time", type: "datetime(6)", nullable: false),
                    groupid = table.Column<long>(name: "group_id", type: "bigint", nullable: false),
                    companyid = table.Column<long>(name: "company_id", type: "bigint", nullable: false),
                    storeid = table.Column<long>(name: "store_id", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_customer_group_id_company_id_store_id",
                table: "customer",
                columns: new[] { "group_id", "company_id", "store_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer");
        }
    }
}
