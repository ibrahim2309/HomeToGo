using Microsoft.AspNetCore.Identity; 

namespace HomeToGo.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public int UserId { get; set; } 
        public virtual User User { get; set; } = default!; 
        public virtual List<ReservationListing>? ReservationListings { get; set; }
        public int ListingId { get; set; }
        public virtual Listing Listing { set; get; } = default!;
        public DateTime CheckInDate { get; set; }  // Check-in date
        public DateTime CheckOutDate { get; set; } // Check-out date
        public int TotalPrice { get; set; }
    }
}