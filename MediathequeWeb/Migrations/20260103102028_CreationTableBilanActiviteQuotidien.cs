using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediathequeWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreationTableStatistiques : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statistiques",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    NouveauxEmprunts = table.Column<int>(type: "INTEGER", nullable: false),
                    RetoursEffectues = table.Column<int>(type: "INTEGER", nullable: false),
                    NouvellesReservations = table.Column<int>(type: "INTEGER", nullable: false),
                    NouveauxAdherents = table.Column<int>(type: "INTEGER", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistiques", x => x.Id);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Statistiques");
        }
    }
}
