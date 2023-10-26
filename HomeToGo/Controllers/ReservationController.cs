using HomeToGo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeToGo.DAL;


namespace HomeToGo.Controllers;

public class ReservationController : Controller
{
    private readonly ListingDbContext _listingDbContext;

    public ReservationController(ListingDbContext listingDbContext)
    {
        _listingDbContext = listingDbContext;
    }

    public async Task<IActionResult> Table()
    {
        List<Reservation> reservations = await _listingDbContext.Reservations.ToListAsync();
        return View(reservations);
    }
}