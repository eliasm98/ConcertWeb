using System.ComponentModel.DataAnnotations;

namespace EventsHandlingService.Models
{
    public class Concert
    {
        [Key] public int ConcertID { get; set; }
        [Required] public string ArtistName { get; set; }
        [Required] public string VenueName { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public int TicketAmount { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
    }
}
