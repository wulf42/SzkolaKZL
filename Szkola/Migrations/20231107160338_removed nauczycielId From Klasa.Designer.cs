﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Szkola.Data;

#nullable disable

namespace Szkola.Migrations
{
    [DbContext(typeof(SzkolaDataContext))]
    [Migration("20231107160338_removed nauczycielId From Klasa")]
    partial class removednauczycielIdFromKlasa
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("Szkola.Models.Klasa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("NauczycielId")
                        .HasColumnType("integer");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NauczycielId");

                    b.ToTable("Klasy");
                });

            modelBuilder.Entity("Szkola.Models.KlasaNauczyciel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("KlasaId")
                        .HasColumnType("integer");

                    b.Property<int>("NauczycielId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("KlasaId");

                    b.HasIndex("NauczycielId");

                    b.ToTable("KlasaNauczyciel");
                });

            modelBuilder.Entity("Szkola.Models.Nauczyciel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataZatrudnienia")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Etat")
                        .HasColumnType("double precision");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Nauczyciele");
                });

            modelBuilder.Entity("Szkola.Models.Uczen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataUrodzenia")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdKlasy")
                        .HasColumnType("integer");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("SredniaPoprzedniRok")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("IdKlasy");

                    b.ToTable("Uczniowie");
                });

            modelBuilder.Entity("Szkola.Models.Klasa", b =>
                {
                    b.HasOne("Szkola.Models.Nauczyciel", "Nauczyciel")
                        .WithMany("Klasy")
                        .HasForeignKey("NauczycielId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nauczyciel");
                });

            modelBuilder.Entity("Szkola.Models.KlasaNauczyciel", b =>
                {
                    b.HasOne("Szkola.Models.Klasa", "Klasa")
                        .WithMany("KlasaNauczyciel")
                        .HasForeignKey("KlasaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Szkola.Models.Nauczyciel", "Nauczyciel")
                        .WithMany("KlasaNauczyciel")
                        .HasForeignKey("NauczycielId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Klasa");

                    b.Navigation("Nauczyciel");
                });

            modelBuilder.Entity("Szkola.Models.Uczen", b =>
                {
                    b.HasOne("Szkola.Models.Klasa", "Klasa")
                        .WithMany("Uczniowie")
                        .HasForeignKey("IdKlasy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Klasa");
                });

            modelBuilder.Entity("Szkola.Models.Klasa", b =>
                {
                    b.Navigation("KlasaNauczyciel");

                    b.Navigation("Uczniowie");
                });

            modelBuilder.Entity("Szkola.Models.Nauczyciel", b =>
                {
                    b.Navigation("KlasaNauczyciel");

                    b.Navigation("Klasy");
                });
#pragma warning restore 612, 618
        }
    }
}
