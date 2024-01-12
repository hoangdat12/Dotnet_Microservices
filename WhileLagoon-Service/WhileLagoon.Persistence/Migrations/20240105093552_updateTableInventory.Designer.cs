﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WhileLagoon.Persistence.DatabaseContext;

#nullable disable

namespace WhileLagoon.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240105093552_updateTableInventory")]
    partial class updateTableInventory
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WhileLagoon.Domain.Entity.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("WhileLagoon.Domain.Entity.CartProduct", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ProductId");

                    b.HasIndex("CartId");

                    b.ToTable("CartProducts");
                });

            modelBuilder.Entity("WhileLagoon.Domain.Entity.Inventory", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("ShopId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ProductId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("WhileLagoon.Domain.Entity.Shop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ShopAvatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("ShopCategory")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("ShopDescription")
                        .HasColumnType("text");

                    b.Property<int>("ShopEvaluate")
                        .HasColumnType("integer");

                    b.Property<int>("ShopFollowers")
                        .HasColumnType("integer");

                    b.Property<int>("ShopFollowing")
                        .HasColumnType("integer");

                    b.Property<string>("ShopName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<List<string>>("ShopOwner")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<int>("ShopTotalProduct")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("WhileLagoon.Domain.Entity.ShopAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AddresState")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AddressCity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AddressCountry")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AddressDetail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AddressPhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AddressUserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ShopId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("ShopAddresses");
                });

            modelBuilder.Entity("WhileLagoon.Domain.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WhileLagoon.Domain.Entity.Cart", b =>
                {
                    b.HasOne("WhileLagoon.Domain.Entity.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("WhileLagoon.Domain.Entity.Cart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WhileLagoon.Domain.Entity.CartProduct", b =>
                {
                    b.HasOne("WhileLagoon.Domain.Entity.Cart", "Cart")
                        .WithMany("Products")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("WhileLagoon.Domain.Entity.ShopAddress", b =>
                {
                    b.HasOne("WhileLagoon.Domain.Entity.Shop", "Shop")
                        .WithMany("Addresses")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("WhileLagoon.Domain.Entity.Cart", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WhileLagoon.Domain.Entity.Shop", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("WhileLagoon.Domain.Entity.User", b =>
                {
                    b.Navigation("Cart");
                });
#pragma warning restore 612, 618
        }
    }
}
