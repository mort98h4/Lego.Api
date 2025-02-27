﻿// <auto-generated />
using Lego.Api.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lego.Api.Migrations
{
    [DbContext(typeof(LegoContext))]
    partial class LegoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("Lego.Api.Entities.Piece", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("PieceNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Pieces");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "Brown",
                            Description = "Fence 1 x 4 x 2 Spindled with 2 Studs",
                            PieceNo = "30055"
                        });
                });

            modelBuilder.Entity("Lego.Api.Entities.Series", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Series");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Small starships made for displaying.",
                            Name = "The Mid-Scale Starship Series"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Large sets made with the utmost care and focus on details.",
                            Name = "Ultimate Collector's Series"
                        });
                });

            modelBuilder.Entity("Lego.Api.Entities.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("HasBox")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IsSealed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ModelNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("NoOfPieces")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SeriesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ThemeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.HasIndex("ThemeId");

                    b.ToTable("Sets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A display piece of a Republic Era assault ship.",
                            HasBox = 1,
                            IsSealed = 1,
                            ModelNo = "75404",
                            Name = "Acclamator-Class Assault Ship",
                            NoOfPieces = 450,
                            SeriesId = 1,
                            ThemeId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "The first edition of Hagrid's Hut.",
                            HasBox = 0,
                            IsSealed = 0,
                            ModelNo = "4707",
                            Name = "Hagrid's Hut",
                            NoOfPieces = 288,
                            ThemeId = 2
                        },
                        new
                        {
                            Id = 3,
                            Description = "This is the AT-AT that alle Lego Star Wars collectors have been waiting for.",
                            HasBox = 1,
                            IsSealed = 0,
                            ModelNo = "75313",
                            Name = "AT-AT",
                            NoOfPieces = 6785,
                            SeriesId = 2,
                            ThemeId = 1
                        });
                });

            modelBuilder.Entity("Lego.Api.Entities.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Themes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "One of the world's most popular franchises.",
                            Name = "Star Wars"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Go on magical adventures with Harry, Ron, and Hermione.",
                            Name = "Harry Potter"
                        });
                });

            modelBuilder.Entity("SetsMissingPieces", b =>
                {
                    b.Property<int>("PieceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SetId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.HasKey("PieceId", "SetId");

                    b.HasIndex("SetId");

                    b.ToTable("SetsMissingPieces");

                    b.HasData(
                        new
                        {
                            PieceId = 1,
                            SetId = 2,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("Lego.Api.Entities.Set", b =>
                {
                    b.HasOne("Lego.Api.Entities.Series", "Series")
                        .WithMany()
                        .HasForeignKey("SeriesId");

                    b.HasOne("Lego.Api.Entities.Theme", "Theme")
                        .WithMany()
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Series");

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("SetsMissingPieces", b =>
                {
                    b.HasOne("Lego.Api.Entities.Piece", null)
                        .WithMany()
                        .HasForeignKey("PieceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lego.Api.Entities.Set", null)
                        .WithMany()
                        .HasForeignKey("SetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
