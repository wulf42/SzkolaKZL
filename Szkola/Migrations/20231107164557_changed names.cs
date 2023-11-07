using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Szkola.Migrations
{
    /// <inheritdoc />
    public partial class changednames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KlasaNauczyciel");

            migrationBuilder.DropTable(
                name: "Uczniowie");

            migrationBuilder.DropTable(
                name: "Klasy");

            migrationBuilder.DropTable(
                name: "Nauczyciele");

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    EmploymentType = table.Column<double>(type: "double precision", nullable: false),
                    DateOfEmployment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClassId = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassTeachers_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastGradePointAverage = table.Column<double>(type: "double precision", nullable: false),
                    IdClass = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Classes_IdClass",
                        column: x => x.IdClass,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeachers_ClassId",
                table: "ClassTeachers",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeachers_TeacherId",
                table: "ClassTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_IdClass",
                table: "Students",
                column: "IdClass");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassTeachers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.CreateTable(
                name: "Nauczyciele",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DataZatrudnienia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Etat = table.Column<double>(type: "double precision", nullable: false),
                    Imie = table.Column<string>(type: "text", nullable: false),
                    Nazwisko = table.Column<string>(type: "text", nullable: false)
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
                    NauczycielId = table.Column<int>(type: "integer", nullable: false),
                    Nazwa = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klasy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Klasy_Nauczyciele_NauczycielId",
                        column: x => x.NauczycielId,
                        principalTable: "Nauczyciele",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Uczniowie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IdKlasy = table.Column<int>(type: "integer", nullable: false),
                    DataUrodzenia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Imie = table.Column<string>(type: "text", nullable: false),
                    Nazwisko = table.Column<string>(type: "text", nullable: false),
                    SredniaPoprzedniRok = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uczniowie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uczniowie_Klasy_IdKlasy",
                        column: x => x.IdKlasy,
                        principalTable: "Klasy",
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

            migrationBuilder.CreateIndex(
                name: "IX_Klasy_NauczycielId",
                table: "Klasy",
                column: "NauczycielId");

            migrationBuilder.CreateIndex(
                name: "IX_Uczniowie_IdKlasy",
                table: "Uczniowie",
                column: "IdKlasy");
        }
    }
}
