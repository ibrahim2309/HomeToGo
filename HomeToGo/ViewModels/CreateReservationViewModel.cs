using Microsoft.AspNetCore.Mvc.Rendering;
using HomeToGo.Models;

namespace HomeToGo.ViewModels;

public class CreateReservationViewModel
{
    public Reservation Reservation { get; set; } = default!;
    public List<SelectListItem> UserSelectList { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> ListingSelectList { get; set; } = new List<SelectListItem>();
}