using Microsoft.AspNetCore.Mvc.Rendering;
using HomeToGo.Models;

namespace HomeToGo.ViewModels;

public class ReservationViewModel
{
    public Reservation Reservation { get; set; }
    public List<ReservationListing> ReservationListings { get; set; } = new List<ReservationListing>();
    public List<SelectListItem> UserSelectList { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> ListingSelectList { get; set; } = new List<SelectListItem>();
}