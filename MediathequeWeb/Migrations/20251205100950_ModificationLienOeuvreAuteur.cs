using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediathequeWeb.Migrations
{
    /// <inheritdoc />
    public partial class ModificationLienOeuvreAuteur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Oeuvres_Auteurs_AuteurId", table: "Oeuvres");

            migrationBuilder.AlterColumn<int>(
                name: "AuteurId",
                table: "Oeuvres",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Oeuvres_Auteurs_AuteurId",
                table: "Oeuvres",
                column: "AuteurId",
                principalTable: "Auteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Oeuvres_Auteurs_AuteurId", table: "Oeuvres");

            migrationBuilder.AlterColumn<int>(
                name: "AuteurId",
                table: "Oeuvres",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Oeuvres_Auteurs_AuteurId",
                table: "Oeuvres",
                column: "AuteurId",
                principalTable: "Auteurs",
                principalColumn: "Id"
            );
        }
    }
}
