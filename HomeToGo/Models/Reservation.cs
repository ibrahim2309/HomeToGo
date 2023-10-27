<<<<<<< Updated upstream
namespace HomeToGo.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public int UserId { get; set; } 
        public virtual User User { get; set; } = default!; 
        public int ListingId { get; set; }
        public virtual Listing Listing { set; get; } = default!;
        public DateTime CheckInDate { get; set; }  // Check-in date
        public DateTime CheckOutDate { get; set; } // Check-out date
        public int TotalPrice { get; set; }
    }
}
=======
using System.ComponentModel.DataAnnotations;
using Microsoft.Build.Framework;
using Microsoft.VisualBasic.CompilerServices;

namespace HomeToGo.Models;

public class Reservation
{
    public int ReservationId { get; set; }
    public int UserId { get; set; }
    public virtual List<User> Users { get; set; }
    public int ListingId { get; set; }
    public List<Listing> Listings { get; set; }
    
    
    [DataType(DataType.Date)]
    [Display(Name = "Check-in Date")]
    // [Required(ErroeMessage = "Check-In Date is required.")] (feil meldimg
    public int CheckInDate { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "Check-out Date")]
    // [Required(ErroeMessage = "Check-In Date is required.")] (feil meldimg
    public int CheckOutDate { get; set; }
}

//input validation er nesten done
>>>>>>> Stashed changes
