using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szkola.Migrations
{
    /// <inheritdoc />
    public partial class dataAnnotationsandimprovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klasy_Nauczyciele_NauczycielId",
                table: "Klasy");

            migrationBuilder.DropForeignKey(
                name: "FK_Uczniowie_Klasy_KlasaId",
                table: "Uczniowie");

            migrationBuilder.DropIndex(
                name: "IX_Uczniowie_KlasaId",
                table: "Uczniowie");

            migrationBuilder.DropColumn(
                name: "KlasaId",
                table: "Uczniowie");

            migrationBuilder.AlterColumn<int>(
                name: "NauczycielId",
                table: "Klasy",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uczniowie_IdKlasy",
                table: "Uczniowie",
                column: "IdKlasy");

            migrationBuilder.AddForeignKey(
                name: "FK_Klasy_Nauczyciele_NauczycielId",
                table: "Klasy",
                column: "NauczycielId",
                principalTable: "Nauczyciele",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Uczniowie_Klasy_IdKlasy",
                table: "Uczniowie",
                column: "IdKlasy",
                principalTable: "Klasy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klasy_Nauczyciele_NauczycielId",
                table: "Klasy");

            migrationBuilder.DropForeignKey(
                name: "FK_Uczniowie_Klasy_IdKlasy",
                table: "Uczniowie");

            migrationBuilder.DropIndex(
                name: "IX_Uczniowie_IdKlasy",
                table: "Uczniowie");

            migrationBuilder.AddColumn<int>(
                name: "KlasaId",
                table: "Uczniowie",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "NauczycielId",
                table: "Klasy",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Uczniowie_KlasaId",
                table: "Uczniowie",
                column: "KlasaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Klasy_Nauczyciele_NauczycielId",
                table: "Klasy",
                column: "NauczycielId",
                principalTable: "Nauczyciele",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Uczniowie_Klasy_KlasaId",
                table: "Uczniowie",
                column: "KlasaId",
                principalTable: "Klasy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
