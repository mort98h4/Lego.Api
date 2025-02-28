﻿// <auto-generated />
using Lego.Api.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lego.Api.Migrations
{
    [DbContext(typeof(LegoContext))]
    [Migration("20250220135245_LegosDBInitialMigration")]
    partial class LegosDBInitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("ModelNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Parts");
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
                });

            modelBuilder.Entity("PartSet", b =>
                {
                    b.Property<int>("MissingPartsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SetsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MissingPartsId", "SetsId");

                    b.HasIndex("SetsId");

                    b.ToTable("PartSet");
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

            modelBuilder.Entity("PartSet", b =>
                {
                    b.HasOne("Lego.Api.Entities.Part", null)
                        .WithMany()
                        .HasForeignKey("MissingPartsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lego.Api.Entities.Set", null)
                        .WithMany()
                        .HasForeignKey("SetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
