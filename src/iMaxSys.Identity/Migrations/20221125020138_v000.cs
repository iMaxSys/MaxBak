using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iMaxSys.Identity.Migrations
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
                name: "check_code",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    xppid = table.Column<long>(name: "xpp_id", type: "bigint", nullable: false),
                    bizid = table.Column<long>(name: "biz_id", type: "bigint", nullable: false),
                    memberid = table.Column<long>(name: "member_id", type: "bigint", nullable: false),
                    to = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    expires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    checkcount = table.Column<int>(name: "check_count", type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_check_code", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    parentid = table.Column<long>(name: "parent_id", type: "bigint", nullable: true, comment: "父节点id"),
                    xppid = table.Column<long>(name: "xpp_id", type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lv = table.Column<int>(type: "int", nullable: false, comment: "左值"),
                    rv = table.Column<int>(type: "int", nullable: false, comment: "右值"),
                    index = table.Column<int>(type: "int", nullable: false, comment: "索引"),
                    level = table.Column<int>(type: "int", nullable: false, comment: "深度"),
                    isroot = table.Column<bool>(name: "is_root", type: "tinyint(1)", nullable: false, comment: "是否根点"),
                    isleaf = table.Column<bool>(name: "is_leaf", type: "tinyint(1)", nullable: false, comment: "是否叶节点"),
                    type = table.Column<int>(type: "int", nullable: false, comment: "类型"),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "Code")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quickcode = table.Column<string>(name: "quick_code", type: "varchar(50)", maxLength: 50, nullable: true, comment: "QuickCode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "是否叶节点")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    style = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "Style")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    selectedstyle = table.Column<string>(name: "selected_style", type: "varchar(50)", maxLength: 50, nullable: true, comment: "SelectedStyle")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "icon")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    selectedicon = table.Column<string>(name: "selected_icon", type: "varchar(50)", maxLength: 50, nullable: true, comment: "SelectedIcon")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ext = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Ext")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isshow = table.Column<bool>(name: "is_show", type: "tinyint(1)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_department", x => x.id);
                    table.ForeignKey(
                        name: "FK_department_department_parent_id",
                        column: x => x.parentid,
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "member_session",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    xppsnsid = table.Column<long>(name: "xpp_sns_id", type: "bigint", nullable: false),
                    memberid = table.Column<long>(name: "member_id", type: "bigint", nullable: false),
                    token = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    unionid = table.Column<string>(name: "union_id", type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    openid = table.Column<string>(name: "open_id", type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sessionkey = table.Column<string>(name: "session_key", type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nickname = table.Column<string>(name: "nick_name", type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    expires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    isofficial = table.Column<bool>(name: "is_official", type: "tinyint(1)", nullable: false),
                    ip = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
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
                    revisetime = table.Column<DateTime>(name: "revise_time", type: "datetime(6)", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_session", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    xppid = table.Column<long>(name: "xpp_id", type: "bigint", nullable: false),
                    parentid = table.Column<long>(name: "parent_id", type: "bigint", nullable: true, comment: "父节点id"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lv = table.Column<int>(type: "int", nullable: false, comment: "左值"),
                    rv = table.Column<int>(type: "int", nullable: false, comment: "右值"),
                    index = table.Column<int>(type: "int", nullable: false, comment: "索引"),
                    level = table.Column<int>(type: "int", nullable: false, comment: "深度"),
                    isroot = table.Column<bool>(name: "is_root", type: "tinyint(1)", nullable: false, comment: "是否根点"),
                    isleaf = table.Column<bool>(name: "is_leaf", type: "tinyint(1)", nullable: false, comment: "是否叶节点"),
                    type = table.Column<int>(type: "int", nullable: false, comment: "类型"),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "Code")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quickcode = table.Column<string>(name: "quick_code", type: "varchar(50)", maxLength: 50, nullable: true, comment: "QuickCode")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "是否叶节点")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    style = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "Style")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    selectedstyle = table.Column<string>(name: "selected_style", type: "varchar(50)", maxLength: 50, nullable: true, comment: "SelectedStyle")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "icon")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    selectedicon = table.Column<string>(name: "selected_icon", type: "varchar(50)", maxLength: 50, nullable: true, comment: "SelectedIcon")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "Action")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ext = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Ext")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false),
                    isshow = table.Column<bool>(name: "is_show", type: "tinyint(1)", nullable: false),
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
                    table.PrimaryKey("PK_menu", x => x.id);
                    table.ForeignKey(
                        name: "FK_menu_menu_parent_id",
                        column: x => x.parentid,
                        principalTable: "menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    xppid = table.Column<long>(name: "xpp_id", type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quickcode = table.Column<string>(name: "quick_code", type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripton = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    style = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    menuids = table.Column<string>(name: "menu_ids", type: "varchar(5000)", maxLength: 5000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    operationids = table.Column<string>(name: "operation_ids", type: "varchar(5000)", maxLength: 5000, nullable: true)
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
                    revisetime = table.Column<DateTime>(name: "revise_time", type: "datetime(6)", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
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
                name: "member",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    userid = table.Column<long>(name: "user_id", type: "bigint", nullable: false, comment: "外接Id"),
                    referrerid = table.Column<long>(name: "referrer_id", type: "bigint", nullable: false, comment: "推荐人Id"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "姓名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idnumber = table.Column<string>(name: "id_number", type: "varchar(50)", maxLength: 50, nullable: false, comment: "身份证号码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quickcode = table.Column<string>(name: "quick_code", type: "varchar(50)", maxLength: 50, nullable: false, comment: "速查码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    birthday = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "生日"),
                    maritalstatus = table.Column<int>(name: "marital_status", type: "int", nullable: false, comment: "婚姻状况"),
                    gender = table.Column<int>(type: "int", nullable: false, comment: "性别(0男,1女,2未知)"),
                    nation = table.Column<int>(type: "int", nullable: false, comment: "民族(默认0:未知)"),
                    education = table.Column<int>(type: "int", nullable: false, comment: "教育程度(默认0:未知)"),
                    party = table.Column<int>(type: "int", nullable: false, comment: "政党(默认0:未知)"),
                    username = table.Column<string>(name: "user_name", type: "varchar(50)", maxLength: 50, nullable: false, comment: "登录名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "密码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    salt = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "盐")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nickname = table.Column<string>(name: "nick_name", type: "varchar(50)", maxLength: 50, nullable: false, comment: "昵称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    countrycode = table.Column<int>(name: "country_code", type: "int", nullable: false, comment: "国家代码(默认中国:86)"),
                    mobile = table.Column<long>(type: "bigint", nullable: false, comment: "移动电话号码"),
                    phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "电话号码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "电子邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "头像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "国家")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    province = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "省")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "市")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    district = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "区/县")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    street = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "街道/乡镇")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    community = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "社区/村")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    areacode = table.Column<long>(name: "area_code", type: "bigint", nullable: false, comment: "区域代码from国家统计局"),
                    address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    zipcode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "邮编")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<int>(type: "int", nullable: false, comment: "类型"),
                    failedcount = table.Column<int>(name: "failed_count", type: "int", nullable: false, comment: "登录失败次数"),
                    departmentid = table.Column<long>(name: "department_id", type: "bigint", nullable: false, comment: "部门id"),
                    xppsource = table.Column<int>(name: "xpp_source", type: "int", nullable: false, comment: "应用来源"),
                    accountsource = table.Column<int>(name: "account_source", type: "int", nullable: false, comment: "账号来源"),
                    start = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "启用时间"),
                    end = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "停用时间"),
                    jointime = table.Column<DateTime>(name: "join_time", type: "datetime(6)", nullable: false, comment: "加入/激活时间"),
                    joinip = table.Column<string>(name: "join_ip", type: "varchar(50)", maxLength: 50, nullable: false, comment: "加入Ip")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastlogin = table.Column<DateTime>(name: "last_login", type: "datetime(6)", nullable: false, comment: "最后登录时间"),
                    lastip = table.Column<string>(name: "last_ip", type: "varchar(50)", maxLength: 50, nullable: false, comment: "最后登录IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    errorcount = table.Column<int>(name: "error_count", type: "int", nullable: false, comment: "错误次数"),
                    islocked = table.Column<bool>(name: "is_locked", type: "tinyint(1)", nullable: false, comment: "是否锁定"),
                    isofficial = table.Column<bool>(name: "is_official", type: "tinyint(1)", nullable: false, comment: "是否正式成员"),
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
                    table.PrimaryKey("PK_member", x => x.id);
                    table.ForeignKey(
                        name: "FK_member_department_department_id",
                        column: x => x.departmentid,
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "operation",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    xppid = table.Column<long>(name: "xpp_id", type: "bigint", nullable: false),
                    menuid = table.Column<long>(name: "menu_id", type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quickcode = table.Column<string>(name: "quick_code", type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripton = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    style = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<int>(type: "int", nullable: false),
                    action = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isshow = table.Column<bool>(name: "is_show", type: "tinyint(1)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_operation", x => x.id);
                    table.ForeignKey(
                        name: "FK_operation_menu_menu_id",
                        column: x => x.menuid,
                        principalTable: "menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "xpp_sns",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    xppid = table.Column<long>(name: "xpp_id", type: "bigint", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "member_ext",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    xppsnsid = table.Column<long>(name: "xpp_sns_id", type: "bigint", nullable: false),
                    memberid = table.Column<long>(name: "member_id", type: "bigint", nullable: false),
                    openid = table.Column<string>(name: "open_id", type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    unionid = table.Column<string>(name: "union_id", type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    token = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    expires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_member_ext", x => x.id);
                    table.ForeignKey(
                        name: "FK_member_ext_member_member_id",
                        column: x => x.memberid,
                        principalTable: "member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role_member",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    roleid = table.Column<long>(name: "role_id", type: "bigint", nullable: false),
                    memberid = table.Column<long>(name: "member_id", type: "bigint", nullable: false),
                    xppid = table.Column<long>(name: "xpp_id", type: "bigint", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_role_member", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_member_member_member_id",
                        column: x => x.memberid,
                        principalTable: "member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_member_role_role_id",
                        column: x => x.roleid,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_check_code_tenant_id",
                table: "check_code",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_check_code_xpp_id_tenant_id_biz_id_member_id_to_expires_stat~",
                table: "check_code",
                columns: new[] { "xpp_id", "tenant_id", "biz_id", "member_id", "to", "expires", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_department_parent_id",
                table: "department",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_department_tenant_id",
                table: "department",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_department_tenant_id_name",
                table: "department",
                columns: new[] { "tenant_id", "name" });

            migrationBuilder.CreateIndex(
                name: "IX_department_tenant_id_quick_code",
                table: "department",
                columns: new[] { "tenant_id", "quick_code" });

            migrationBuilder.CreateIndex(
                name: "IX_member_department_id",
                table: "member",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_member_id_number",
                table: "member",
                column: "id_number");

            migrationBuilder.CreateIndex(
                name: "IX_member_mobile",
                table: "member",
                column: "mobile");

            migrationBuilder.CreateIndex(
                name: "IX_member_name",
                table: "member",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_member_tenant_id",
                table: "member",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_member_user_id",
                table: "member",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_member_user_name",
                table: "member",
                column: "user_name");

            migrationBuilder.CreateIndex(
                name: "IX_member_ext_member_id",
                table: "member_ext",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_member_ext_tenant_id",
                table: "member_ext",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_member_ext_xpp_sns_id_open_id",
                table: "member_ext",
                columns: new[] { "xpp_sns_id", "open_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_member_ext_xpp_sns_id_union_id",
                table: "member_ext",
                columns: new[] { "xpp_sns_id", "union_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_member_session_tenant_id",
                table: "member_session",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_member_session_xpp_sns_id_member_id",
                table: "member_session",
                columns: new[] { "xpp_sns_id", "member_id" });

            migrationBuilder.CreateIndex(
                name: "IX_member_session_xpp_sns_id_token",
                table: "member_session",
                columns: new[] { "xpp_sns_id", "token" });

            migrationBuilder.CreateIndex(
                name: "IX_menu_parent_id",
                table: "menu",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_menu_tenant_id",
                table: "menu",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_menu_tenant_id_xpp_id",
                table: "menu",
                columns: new[] { "tenant_id", "xpp_id" });

            migrationBuilder.CreateIndex(
                name: "IX_operation_menu_id",
                table: "operation",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_operation_tenant_id",
                table: "operation",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_operation_tenant_id_xpp_id",
                table: "operation",
                columns: new[] { "tenant_id", "xpp_id" });

            migrationBuilder.CreateIndex(
                name: "IX_role_tenant_id",
                table: "role",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_member_member_id",
                table: "role_member",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_member_role_id",
                table: "role_member",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_member_tenant_id",
                table: "role_member",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_member_tenant_id_member_id_role_id_xpp_id",
                table: "role_member",
                columns: new[] { "tenant_id", "member_id", "role_id", "xpp_id" });

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
                name: "check_code");

            migrationBuilder.DropTable(
                name: "member_ext");

            migrationBuilder.DropTable(
                name: "member_session");

            migrationBuilder.DropTable(
                name: "operation");

            migrationBuilder.DropTable(
                name: "role_member");

            migrationBuilder.DropTable(
                name: "tenant");

            migrationBuilder.DropTable(
                name: "xpp_sns");

            migrationBuilder.DropTable(
                name: "menu");

            migrationBuilder.DropTable(
                name: "member");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "xpp");

            migrationBuilder.DropTable(
                name: "department");
        }
    }
}
