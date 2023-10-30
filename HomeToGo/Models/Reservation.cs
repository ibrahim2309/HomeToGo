using System.ComponentModel.DataAnnotations;
using Microsoft.Build.Framework;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.AspNetCore.Identity;
using System;


namespace HomeToGo.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Today; 
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
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