using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsHandlingService.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingtoDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcertID = table.Column<int>(type: "int", nullable: false),
                    TicketNb = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.UpdateData(
                table: "Concerts",
                keyColumn: "ConcertID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 5, 13, 15, 16, 58, 989, DateTimeKind.Local).AddTicks(420));

            migrationBuilder.UpdateData(
                table: "Concerts",
                keyColumn: "ConcertID",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 5, 13, 15, 16, 58, 989, DateTimeKind.Local).AddTicks(577));
        }
    }
}
