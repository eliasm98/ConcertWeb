using System.ComponentModel.DataAnnotations;

namespace EventsHandlingService.Models
{
    public class Booking
    {
        [Key] public int BookingId { get; set; }
        [Required] public int ConcertID { get; set; }
        [Required] public int UserId { get; set; }
        [Required] public int TicketNb { get; set; }
    }
}
