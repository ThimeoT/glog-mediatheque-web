using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediathequeWeb.Migrations
{
    /// <inheritdoc />
    public partial class ModificationDateEmprunt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateEmprunt",
                table: "Emprunts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEmprunt",
                table: "Emprunts");
        }
    }
}
