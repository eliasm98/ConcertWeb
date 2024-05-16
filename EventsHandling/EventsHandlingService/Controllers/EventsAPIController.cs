using EventsHandlingService.Data;
using EventsHandlingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAuthenticationAPI.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EventsHandlingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsAPIController : ControllerBase
    {
        private readonly EventsDbContext _db;
        private readonly IHttpClientFactory _httpClientFactory;
        public EventsAPIController(EventsDbContext db, IHttpClientFactory httpClientFactory)
        {
            _db = db;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("GetAllConcerts")]
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        public object GetbyArtist(string Name)
        {
            try
            {
                Concert objList = _db.Concerts.First(u => u.ArtistName.Contains(Name));
                return objList;
            }
            catch (Exception ex)
            { }
            return null;

        }

        [HttpGet]
        [Route("GetConcertByVenue/{Venue}")]
        [AllowAnonymous]
        public object GetbyVenue(string Venue)
        {
            try
            {
                Concert objList = _db.Concerts.First(u => u.VenueName.Contains(Venue));
                return objList;
            }
            catch (Exception ex)
            { }
            return null;

        }
        [HttpPost]
        [Route("CreateConcert")]
        [Authorize(Policy = "AdminOnly")]
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
        [Authorize(Policy = "AdminOnly")]
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
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> PostBooking([FromBody] Booking booking, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return BadRequest("Authorization header is missing");
            }

            string token = authorizationHeader.Replace("Bearer ", "");
            int id;
            try
            {
                JwtSecurityToken decodedToken = JwtDecoder.DecodeToken(token);
                id = Convert.ToInt32(JwtDecoder.GetClaimValue(decodedToken, "nameid"));
            }
            catch (Exception)
            {
                return Unauthorized("Invalid token");
            }

            var concert = await _db.Concerts.FindAsync(booking.ConcertID);
            if (concert == null)
            {
                return NotFound("Concert not found");
            }

            int bookingPrice = concert.Price;

            var paymentRequest = new PaymentRequest
            {
                UserId = id,
                Price = bookingPrice
            };

            try
            {
                Console.WriteLine("UserID: " + paymentRequest.UserId);
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await httpClient.PatchAsJsonAsync("http://localhost:5016/process-payment", paymentRequest);

                    if (response.IsSuccessStatusCode)
                    {
                        booking.BookingId = 0;
                        booking.UserId = id;

                        _db.Bookings.Add(booking);
                        int saved = await _db.SaveChangesAsync();

                        if (saved > 0)
                        {
                            return Ok(booking);
                        }
                        else
                        {
                            return StatusCode(500, "Failed to save booking to the database.");
                        }
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        return BadRequest($"Payment failed: {errorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the payment: {ex.Message}");
            }
        }



        [HttpGet]
        [Route("GetBookingsForConcert/{id}")]
        [Authorize(Policy = "AdminOnly")]
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

        [HttpGet]
        [Route("GetUserBookings")]
        [Authorize(Policy = "UserOnly")]
        public object GetUserBookings([FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            string token = authorizationHeader.Replace("Bearer ", "");
            try
            {
                JwtSecurityToken decodedToken = JwtDecoder.DecodeToken(token);
                int id = Convert.ToInt32(JwtDecoder.GetClaimValue(decodedToken, "nameid"));
                var userBookings = _db.Bookings
                    .Where(booking => booking.UserId == id)
                    .Join(
                        _db.Concerts,
                        booking => booking.ConcertID,
                        concert => concert.ConcertID,
                        (booking, concert) => new
                        {
                            BookingID = booking.BookingId,
                            ConcertID = concert.ConcertID,
                            ArtistName = concert.ArtistName,
                            Genre = concert.Genre,
                            VenueName = concert.VenueName,
                            Duration = concert.Duration,
                            TicketAmount = concert.TicketAmount,
                            Price = concert.Price,
                            Date = concert.Date
                        }
                    );
                return userBookings;
            }
            catch (Exception ex)
            { }
            return null;

        }


    }
}
