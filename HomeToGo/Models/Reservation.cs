namespace HomeToGo.Models;

public class Reservation
{
    public int ReservationId { get; set; }
    public int UserId { get; set; }
    public virtual List<User> Users { get; set; }
    public int ListingId { get; set; }
    public List<Listing> Listings { get; set; }
    public int CheckInDate { get; set; }
    public int CheckOutDate { get; set; }
}