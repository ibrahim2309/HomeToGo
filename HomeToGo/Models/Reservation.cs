namespace HomeToGo.Models;

public class Reservation
{
    public int ReservationId { get; set; }
    public string ReservationDate { get; set; } = string.Empty;
    public int UserId { get; set; }
    
    public virtual User User { set; get; } = default!;
    public int ListingId { get; set; }
    public virtual List<Listing>? Listings { get; set; }
    public int CheckInDate { get; set; }
    public int CheckOutDate { get; set; }
    public int TotalPrice { get; set; }

}