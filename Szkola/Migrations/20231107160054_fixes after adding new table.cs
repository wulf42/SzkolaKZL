using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Szkola.Migrations
{
    /// <inheritdoc />
    public partial class fixesafteraddingnewtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KlasaNauczyciel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    KlasaId = table.Column<int>(type: "integer", nullable: false),
                    NauczycielId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlasaNauczyciel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KlasaNauczyciel_Klasy_KlasaId",
                        column: x => x.KlasaId,
                        principalTable: "Klasy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KlasaNauczyciel_Nauczyciele_NauczycielId",
                        column: x => x.NauczycielId,
                        principalTable: "Nauczyciele",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KlasaNauczyciel_KlasaId",
                table: "KlasaNauczyciel",
                column: "KlasaId");

            migrationBuilder.CreateIndex(
                name: "IX_KlasaNauczyciel_NauczycielId",
                table: "KlasaNauczyciel",
                column: "NauczycielId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KlasaNauczyciel");
        }
    }
}
