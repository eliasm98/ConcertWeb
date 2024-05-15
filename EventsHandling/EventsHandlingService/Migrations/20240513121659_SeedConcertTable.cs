using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventsHandlingService.Migrations
{
    /// <inheritdoc />
    public partial class SeedConcertTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "ConcertID", "ArtistName", "Date", "Duration", "Genre", "Price", "TicketAmount", "VenueName" },
                values: new object[,]
                {
                    { 1, "Adele", new DateTime(2024, 5, 13, 15, 16, 58, 989, DateTimeKind.Local).AddTicks(420), 90, "Pop", 50, 100, "Olympic Stage" },
                    { 2, "Maroon 5", new DateTime(2024, 5, 13, 15, 16, 58, 989, DateTimeKind.Local).AddTicks(577), 120, "Rock", 40, 50, "O2 Center" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "ConcertID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "ConcertID",
                keyValue: 2);
        }
    }
}
