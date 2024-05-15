﻿// <auto-generated />
using System;
using EventsHandlingService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventsHandlingService.Migrations
{
    [DbContext(typeof(EventsDbContext))]
    [Migration("20240513142203_SeedBookingTables")]
    partial class SeedBookingTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventsHandlingService.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<int>("ConcertID")
                        .HasColumnType("int");

                    b.Property<int>("TicketNb")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            BookingId = 100001,
                            ConcertID = 1,
                            TicketNb = 2
                        },
                        new
                        {
                            BookingId = 100002,
                            ConcertID = 2,
                            TicketNb = 5
                        });
                });

            modelBuilder.Entity("EventsHandlingService.Models.Concert", b =>
                {
                    b.Property<int>("ConcertID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConcertID"));

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("TicketAmount")
                        .HasColumnType("int");

                    b.Property<string>("VenueName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConcertID");

                    b.ToTable("Concerts");

                    b.HasData(
                        new
                        {
                            ConcertID = 1,
                            ArtistName = "Adele",
                            Date = new DateTime(2024, 5, 13, 17, 22, 3, 369, DateTimeKind.Local).AddTicks(6023),
                            Duration = 90,
                            Genre = "Pop",
                            Price = 50,
                            TicketAmount = 100,
                            VenueName = "Olympic Stage"
                        },
                        new
                        {
                            ConcertID = 2,
                            ArtistName = "Maroon 5",
                            Date = new DateTime(2024, 5, 13, 17, 22, 3, 369, DateTimeKind.Local).AddTicks(6137),
                            Duration = 120,
                            Genre = "Rock",
                            Price = 40,
                            TicketAmount = 50,
                            VenueName = "O2 Center"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
