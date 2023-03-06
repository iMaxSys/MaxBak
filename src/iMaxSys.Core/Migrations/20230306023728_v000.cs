﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iMaxSys.Core.Migrations
{
    /// <inheritdoc />
    public partial class v000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "license",
                table: "tenant",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "license",
                table: "tenant");
        }
    }
}
