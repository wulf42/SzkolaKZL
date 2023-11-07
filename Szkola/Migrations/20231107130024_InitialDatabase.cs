using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Szkola.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nauczyciele",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Imie = table.Column<string>(type: "text", nullable: false),
                    Nazwisko = table.Column<string>(type: "text", nullable: false),
                    Etat = table.Column<double>(type: "double precision", nullable: false),
                    DataZatrudnienia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nauczyciele", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Klasy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nazwa = table.Column<string>(type: "text", nullable: false),
                    NauczycielId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klasy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Klasy_Nauczyciele_NauczycielId",
                        column: x => x.NauczycielId,
                        principalTable: "Nauczyciele",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Uczniowie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Imie = table.Column<string>(type: "text", nullable: false),
                    Nazwisko = table.Column<string>(type: "text", nullable: false),
                    DataUrodzenia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SredniaPoprzedniRok = table.Column<double>(type: "double precision", nullable: false),
                    IdKlasy = table.Column<int>(type: "integer", nullable: false),
                    KlasaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uczniowie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uczniowie_Klasy_KlasaId",
                        column: x => x.KlasaId,
                        principalTable: "Klasy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Klasy_NauczycielId",
                table: "Klasy",
                column: "NauczycielId");

            migrationBuilder.CreateIndex(
                name: "IX_Uczniowie_KlasaId",
                table: "Uczniowie",
                column: "KlasaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uczniowie");

            migrationBuilder.DropTable(
                name: "Klasy");

            migrationBuilder.DropTable(
                name: "Nauczyciele");
        }
    }
}
