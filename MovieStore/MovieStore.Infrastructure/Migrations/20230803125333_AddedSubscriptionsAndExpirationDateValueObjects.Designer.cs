﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieStore.Infrastructure;

#nullable disable

namespace MovieStore.Infrastructure.Migrations
{
    [DbContext(typeof(MovieStoreContext))]
    [Migration("20230803125333_AddedSubscriptionsAndExpirationDateValueObjects")]
    partial class AddedSubscriptionsAndExpirationDateValueObjects
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MovieStore.Core.Model.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MoneySpent")
                        .HasColumnType("float");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Subscription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MovieStore.Core.Model.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("LicensingType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieStore.Core.Model.PurchasedMovie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MovieSubscription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MovieId");

                    b.ToTable("PurchasedMovies");
                });

            modelBuilder.Entity("MovieStore.Core.Model.PurchasedMovie", b =>
                {
                    b.HasOne("MovieStore.Core.Model.Customer", "Customer")
                        .WithMany("PurchasedMovies")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieStore.Core.Model.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieStore.Core.Model.Customer", b =>
                {
                    b.Navigation("PurchasedMovies");
                });
#pragma warning restore 612, 618
        }
    }
}
