using Microsoft.AspNetCore.Mvc.Rendering;
using HomeToGo.Models;

namespace HomeToGo.ViewModels;

public class CreateReservationViewModel
{
    public Reservation Reservation { get; set; } = default!;
    public List<SelectListItem> UserSelectList { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> ListingSelectList { get; set; } = new List<SelectListItem>();
    
    /*

    public decimal CalculateTotalPrice(Listing listing, Reservation reservation)
    {
        if (listing == null || reservation == null)
        {
            throw new ArgumentNullException("Listing or Reservation cannot be null");
        }

        if (reservation.CheckOutDate <= reservation.CheckInDate)
        {
            throw new ArgumentException("CheckOutDate must be greater than CheckInDate");
        }

        TimeSpan stayDuration = reservation.CheckOutDate - reservation.CheckInDate;
        decimal totalPrice = listing.Price * stayDuration.Days;

        // If you want to charge for the check-out day as well, uncomment the next line
        // totalPrice += listing.Price;

        return totalPrice;
    }
    */

}