﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PicturyMarket.DAL;

#nullable disable

namespace PicturyMarket.DAL.Migrations
{
    [DbContext(typeof(PicturyMarketDbContext))]
    [Migration("20230128010146_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PicturyMarket.Domain.Entity.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Baskets", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("PicturyMarket.Domain.Entity.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BasketId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PicturyId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("PicturyMarket.Domain.Entity.Pictury", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("bytea");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Genre")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Picturies", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreate = new DateTime(2023, 1, 28, 4, 1, 46, 116, DateTimeKind.Utc).AddTicks(7963),
                            Description = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                            Genre = 0,
                            Price = 10000m,
                            Title = "Alex"
                        });
                });

            modelBuilder.Entity("PicturyMarket.Domain.Entity.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte>("Age")
                        .HasColumnType("smallint");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = (byte)0,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("PicturyMarket.Domain.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Alex",
                            Password = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f",
                            Role = 2
                        });
                });

            modelBuilder.Entity("PicturyMarket.Domain.Entity.Basket", b =>
                {
                    b.HasOne("PicturyMarket.Domain.Entity.User", "Users")
                        .WithOne("Basket")
                        .HasForeignKey("PicturyMarket.Domain.Entity.Basket", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PicturyMarket.Domain.Entity.Order", b =>
                {
                    b.HasOne("PicturyMarket.Domain.Entity.Basket", "Basket")
                        .WithMany("Orders")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");
                });

            modelBuilder.Entity("PicturyMarket.Domain.Entity.Pictury", b =>
                {
                    b.HasOne("PicturyMarket.Domain.Entity.User", null)
                        .WithMany("Picturies")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PicturyMarket.Domain.Entity.Profile", b =>
                {
                    b.HasOne("PicturyMarket.Domain.Entity.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("PicturyMarket.Domain.Entity.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PicturyMarket.Domain.Entity.Basket", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PicturyMarket.Domain.Entity.User", b =>
                {
                    b.Navigation("Basket")
                        .IsRequired();

                    b.Navigation("Picturies");

                    b.Navigation("Profile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}