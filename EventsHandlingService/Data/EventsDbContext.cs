using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using System;
using EventsHandlingService.Models;

namespace EventsHandlingService.Data
{
    public class EventsDbContext : DbContext
    {
        public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options)
        {
        }

        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Concert>().HasData(new Concert
            {
                ConcertID = 1,
                ArtistName = "Adele",
                VenueName = "Olympic Stage",
                Genre = "Pop",
                Duration = 90,
                TicketAmount = 100,
                Price = 50,
                Date = DateTime.Now
            });

            modelBuilder.Entity<Concert>().HasData(new Concert
            {
                ConcertID = 2,
                ArtistName = "Maroon 5",
                VenueName = "O2 Center",
                Genre = "Rock",
                Duration = 120,
                TicketAmount = 50,
                Price = 40,
                Date = DateTime.Now
            });


            modelBuilder.Entity<Booking>().HasData(new Booking
            {
                BookingId = 100001,
                ConcertID = 1,
                TicketNb = 2
            });

            modelBuilder.Entity<Booking>().HasData(new Booking
            {
                BookingId = 100002,
                ConcertID = 2,
                TicketNb = 5
            });
        }

    }
}
