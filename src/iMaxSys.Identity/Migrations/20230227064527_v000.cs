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
            migrationBuilder.AddColumn<long>(
                name: "xpp_id",
                table: "tenant_menu",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xpp_id",
                table: "tenant_menu");
        }
    }
}
