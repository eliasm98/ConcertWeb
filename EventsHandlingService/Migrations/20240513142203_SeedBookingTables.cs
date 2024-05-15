using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventsHandlingService.Migrations
{
    /// <inheritdoc />
    public partial class SeedBookingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "ConcertID", "TicketNb" },
                values: new object[,]
                {
                    { 100001, 1, 2 },
                    { 100002, 2, 5 }
                });

            migrationBuilder.AddForeignKey(
                 name: "FK_Bookings_Concerts_ConcertID", // Define a meaningful foreign key name
                 table: "Bookings",
                 column: "ConcertID",
                 principalTable: "Concerts",
                 principalColumn: "ConcertID",
                 onDelete: ReferentialAction.Restrict // Define the action on delete (Restrict, Cascade, etc.)
                );

            migrationBuilder.UpdateData(
                table: "Concerts",
                keyColumn: "ConcertID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 5, 13, 17, 22, 3, 369, DateTimeKind.Local).AddTicks(6023));

            migrationBuilder.UpdateData(
                table: "Concerts",
                keyColumn: "ConcertID",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 5, 13, 17, 22, 3, 369, DateTimeKind.Local).AddTicks(6137));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 100001);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 100002);

            migrationBuilder.UpdateData(
                table: "Concerts",
                keyColumn: "ConcertID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 5, 13, 17, 11, 9, 832, DateTimeKind.Local).AddTicks(6959));

            migrationBuilder.UpdateData(
                table: "Concerts",
                keyColumn: "ConcertID",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 5, 13, 17, 11, 9, 832, DateTimeKind.Local).AddTicks(7045));
        }
    }
}
