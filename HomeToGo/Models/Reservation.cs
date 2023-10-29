using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HomeToGo.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
<<<<<<< Updated upstream
        public DateTime ReservationDate { get; set; } = DateTime.Today; 
        public int UserId { get; set; } 
        public virtual User User { get; set; } = default!; 
=======
        public DateTime ReservationDate { get; set; } = DateTime.Now;

        // Reference to the AspNetUser
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        
        [Display(Name = "Listing")]
>>>>>>> Stashed changes
        public int ListingId { get; set; }
        public virtual Listing Listing { set; get; } = default!;
        
        
        [DataType(DataType.Date)]
        [Display(Name = "Check-in Date")]
        public DateTime CheckInDate { get; set; }  // Check-in date
        
        [DataType(DataType.Date)]
        [Display(Name = "Check-out Date")]
        public DateTime CheckOutDate { get; set; } // Check-out date
        
        public decimal TotalPrice { get; set; }
    }
}