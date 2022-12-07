using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iMaxSys.Core.Migrations
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
                name: "dict",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "别名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "编号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quickcode = table.Column<string>(name: "quick_code", type: "varchar(50)", maxLength: 50, nullable: false, comment: "速查码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    datatype = table.Column<int>(name: "data_type", type: "int", nullable: false, comment: "数据类型"),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "value")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    thumbnail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "缩略图")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "图像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    style = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "style")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ordinal = table.Column<int>(type: "int", nullable: false, comment: "序号"),
                    editable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "可编辑"),
                    xppid = table.Column<long>(name: "xpp_id", type: "bigint", nullable: false, comment: "XppId"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "tinyint(1)", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false),
                    creatorid = table.Column<long>(name: "creator_id", type: "bigint", nullable: false),
                    creator = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reviserid = table.Column<long>(name: "reviser_id", type: "bigint", nullable: false),
                    reviser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    revisetime = table.Column<DateTime>(name: "revise_time", type: "datetime(6)", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dict", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tenant",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quickcode = table.Column<string>(name: "quick_code", type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    level = table.Column<int>(type: "int", nullable: false),
                    logo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mail = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    end = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "tinyint(1)", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false),
                    creatorid = table.Column<long>(name: "creator_id", type: "bigint", nullable: false),
                    creator = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reviserid = table.Column<long>(name: "reviser_id", type: "bigint", nullable: false),
                    reviser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    revisetime = table.Column<DateTime>(name: "revise_time", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "xpp",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    needmobile = table.Column<bool>(name: "need_mobile", type: "tinyint(1)", nullable: false),
                    source = table.Column<int>(type: "int", nullable: false),
                    accountid = table.Column<string>(name: "account_id", type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    appid = table.Column<string>(name: "app_id", type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    appkey = table.Column<string>(name: "app_key", type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    host = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "tinyint(1)", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false),
                    creatorid = table.Column<long>(name: "creator_id", type: "bigint", nullable: false),
                    creator = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reviserid = table.Column<long>(name: "reviser_id", type: "bigint", nullable: false),
                    reviser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    revisetime = table.Column<DateTime>(name: "revise_time", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xpp", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dict_item",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    DictId = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "别名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "编号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quickcode = table.Column<string>(name: "quick_code", type: "varchar(50)", maxLength: 50, nullable: false, comment: "速查码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "value")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    thumbnail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "缩略图")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "图像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    style = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "style")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ordinal = table.Column<int>(type: "int", nullable: false, comment: "序号"),
                    editable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "可编辑"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "tinyint(1)", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false),
                    creatorid = table.Column<long>(name: "creator_id", type: "bigint", nullable: false),
                    creator = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reviserid = table.Column<long>(name: "reviser_id", type: "bigint", nullable: false),
                    reviser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    revisetime = table.Column<DateTime>(name: "revise_time", type: "datetime(6)", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dict_item", x => x.id);
                    table.ForeignKey(
                        name: "FK_dict_item_dict_DictId",
                        column: x => x.DictId,
                        principalTable: "dict",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "xpp_sns",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    xppid = table.Column<long>(name: "xpp_id", type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    source = table.Column<int>(type: "int", nullable: false),
                    accountid = table.Column<string>(name: "account_id", type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    appid = table.Column<string>(name: "app_id", type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    appkey = table.Column<string>(name: "app_key", type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "tinyint(1)", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: false),
                    creatorid = table.Column<long>(name: "creator_id", type: "bigint", nullable: false),
                    creator = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reviserid = table.Column<long>(name: "reviser_id", type: "bigint", nullable: false),
                    reviser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    revisetime = table.Column<DateTime>(name: "revise_time", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xpp_sns", x => x.id);
                    table.ForeignKey(
                        name: "FK_xpp_sns_xpp_xpp_id",
                        column: x => x.xppid,
                        principalTable: "xpp",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_dict_name_quick_code",
                table: "dict",
                columns: new[] { "name", "quick_code" });

            migrationBuilder.CreateIndex(
                name: "IX_dict_tenant_id",
                table: "dict",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_dict_item_DictId",
                table: "dict_item",
                column: "DictId");

            migrationBuilder.CreateIndex(
                name: "IX_dict_item_tenant_id",
                table: "dict_item",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_tenant_name_code",
                table: "tenant",
                columns: new[] { "name", "code" });

            migrationBuilder.CreateIndex(
                name: "IX_xpp_name",
                table: "xpp",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_xpp_sns_name",
                table: "xpp_sns",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_xpp_sns_xpp_id",
                table: "xpp_sns",
                column: "xpp_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dict_item");

            migrationBuilder.DropTable(
                name: "tenant");

            migrationBuilder.DropTable(
                name: "xpp_sns");

            migrationBuilder.DropTable(
                name: "dict");

            migrationBuilder.DropTable(
                name: "xpp");
        }
    }
}
