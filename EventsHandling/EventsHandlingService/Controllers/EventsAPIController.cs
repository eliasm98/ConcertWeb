using EventsHandlingService.Data;
using EventsHandlingService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsHandlingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsAPIController : ControllerBase
    {
        private readonly EventsDbContext _db;
        public EventsAPIController(EventsDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("GetAllConcerts")]
        public object GetAll()
        {
            try
            {
                IEnumerable<Concert> objList = _db.Concerts.ToList();
                return objList;
            }
            catch(Exception ex) 
            { }
            return null;

        }


        [HttpGet]
        [Route("GetConcertByID/{id}")]
        public object GetbyID(int id)
        {
            try
            {
                Concert objList = _db.Concerts.First(u => u.ConcertID ==id);
                return objList;
            }
            catch (Exception ex)
            { }
            return null;

        }

        [HttpGet]
        [Route("GetConcertByArtist/{Name}")]
        public object GetbyArtist(string Name)
        {
            try
            {
                Concert objList = _db.Concerts.First(u => u.ArtistName == Name);
                return objList;
            }
            catch (Exception ex)
            { }
            return null;

        }

        [HttpGet]
        [Route("GetConcertByVenue/{Venue}")]
        public object GetbyVenue(string Venue)
        {
            try
            {
                Concert objList = _db.Concerts.First(u => u.VenueName == Venue);
                return objList;
            }
            catch (Exception ex)
            { }
            return null;

        }
        [HttpPost]
        [Route("CreateConcert")]
        public IActionResult PostConcert([FromBody] Concert concert)
        {
            try
            {
                concert.ConcertID = 0;
                _db.Concerts.Add(concert);
                int saved = _db.SaveChanges();
                if (saved > 0)
                {
                    return Ok(concert); // Return 200 OK with the created concert
                }
                else
                {
                    return StatusCode(500, "Failed to save concert to the database."); // Return 500 Internal Server Error if no rows were affected
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while saving the concert: {ex.Message}"); // Return 500 Internal Server Error with the exception message
            }
        }


        [HttpPut]
        [Route("UpdateConcert")]
        public IActionResult PutConcert([FromBody] Concert concert)
        {
            try
            {
                _db.Concerts.Update(concert);
                int saved = _db.SaveChanges();
                if (saved > 0)
                {
                    return Ok(concert); // Return 200 OK with the created concert
                }
                else
                {
                    return StatusCode(500, "Failed to save concert to the database."); // Return 500 Internal Server Error if no rows were affected
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while saving the concert: {ex.Message}"); // Return 500 Internal Server Error with the exception message
            }
        }



        [HttpPost]
        [Route("CreateBooking")]
        public IActionResult PostBooking([FromBody] Booking booking)
        {
            try
            {
                booking.BookingId = 0;
                _db.Bookings.Add(booking);
                int saved = _db.SaveChanges();
                if (saved > 0)
                {
                    return Ok(booking); // Return 200 OK with the created concert
                }
                else
                {
                    return StatusCode(500, "Failed to save booking to the database."); // Return 500 Internal Server Error if no rows were affected
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while saving the booking: {ex.Message}"); // Return 500 Internal Server Error with the exception message
            }
        }


        [HttpGet]
        [Route("GetBookingsForConcert/{id}")]
        public object GetBookingsbyID(int id)
        {
            try
            {
                IEnumerable<Booking> objList = _db.Bookings.Where(u => u.ConcertID == id);
                return objList;
            }
            catch (Exception ex)
            { }
            return null;

        }


    }
}
