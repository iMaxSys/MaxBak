using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iMaxSys.Identity.Migrations
{
    public partial class v000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_xpp_sns_name",
                table: "xpp_sns",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_xpp_name",
                table: "xpp",
                column: "name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_xpp_sns_name",
                table: "xpp_sns");

            migrationBuilder.DropIndex(
                name: "IX_xpp_name",
                table: "xpp");
        }
    }
}
