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

            modelBuilder.Entity("Lego.Api.Entities.Collection", b =>
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

                    b.ToTable("Collections");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Small starships made for displaying.",
                            Name = "The Mid-Scale Starship Collection"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Large sets made with the utmost care and focus on details.",
                            Name = "Ultimate Collector's Series"
                        });
                });

            modelBuilder.Entity("Lego.Api.Entities.Part", b =>
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

                    b.Property<string>("PartNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SetId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SetId");

                    b.ToTable("Parts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "Brown",
                            Description = "Fence 1 x 4 x 2 Spindled with 2 Studs",
                            PartNo = "30055",
                            Quantity = 1,
                            SetId = 2
                        });
                });

            modelBuilder.Entity("Lego.Api.Entities.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CollectionId")
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

                    b.Property<int>("NoOfParts")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ThemeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.HasIndex("ThemeId");

                    b.ToTable("Sets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CollectionId = 1,
                            Description = "A display piece of a Republic Era assault ship.",
                            HasBox = 1,
                            IsSealed = 1,
                            ModelNo = "75404",
                            Name = "Acclamator-Class Assault Ship",
                            NoOfParts = 450,
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
                            NoOfParts = 288,
                            ThemeId = 2
                        },
                        new
                        {
                            Id = 3,
                            CollectionId = 2,
                            Description = "This is the AT-AT that alle Lego Star Wars collectors have been waiting for.",
                            HasBox = 1,
                            IsSealed = 0,
                            ModelNo = "75313",
                            Name = "AT-AT",
                            NoOfParts = 6785,
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

            modelBuilder.Entity("Lego.Api.Entities.Part", b =>
                {
                    b.HasOne("Lego.Api.Entities.Set", "Set")
                        .WithMany("MissingParts")
                        .HasForeignKey("SetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Set");
                });

            modelBuilder.Entity("Lego.Api.Entities.Set", b =>
                {
                    b.HasOne("Lego.Api.Entities.Collection", "Collection")
                        .WithMany()
                        .HasForeignKey("CollectionId");

                    b.HasOne("Lego.Api.Entities.Theme", "Theme")
                        .WithMany()
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("Lego.Api.Entities.Set", b =>
                {
                    b.Navigation("MissingParts");
                });
#pragma warning restore 612, 618
        }
    }
}
