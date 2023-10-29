using HomeToGo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeToGo.DAL;
using HomeToGo.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace HomeToGo.Controllers;

public class ReservationController : Controller
{
    private readonly ILogger<ReservationController> _logger; 
    private readonly ListingDbContext _listingDbContext;

    public ReservationController(ListingDbContext listingDbContext, ILogger<ReservationController> logger)
    {
        _listingDbContext = listingDbContext;
        _logger = logger;
    }

    private async Task CalculateTotalPrice(Reservation reservation)
    {
        if (reservation == null)
        {
            throw new ArgumentNullException(nameof(reservation), "Reservation cannot be null");
        }

        if (reservation.ListingId == null)
        {
            throw new ArgumentException("Reservation must have a ListingId");
        }

        var listing = await _listingDbContext.Listings
            .FindAsync(reservation.ListingId);

        if (listing == null)
        {
            throw new ArgumentException("Invalid ListingId in Reservation");
        }
        


        /*
        if (reservation.CheckOutDate <= reservation.CheckInDate)
        {
            throw new ArgumentException("CheckOutDate must be greater than CheckInDate");
        }
*/
        TimeSpan stayDuration = reservation.CheckOutDate - reservation.CheckInDate;
        reservation.TotalPrice = listing.Price * stayDuration.Days;
    }

    
    

    
    public async Task<IActionResult> Table()
    {
        List<Reservation> reservations = await _listingDbContext.Reservations
            .Include(r => r.Listing)
            .ToListAsync();

        foreach (var reservation in reservations)
        {
            await CalculateTotalPrice(reservation);
        }

        return View(reservations);
    }


    
    

   

    
    [HttpGet]
    public async Task<IActionResult> CreateReservation()
    {
        
        var users = await _listingDbContext.Users.ToListAsync();
        var listings = await _listingDbContext.Listings.ToListAsync();

        var createReservationViewModel = new CreateReservationViewModel
        {
            Reservation = new Reservation(),

            UserSelectList = users.Select(user => new SelectListItem
            {
                Value = user.UserId.ToString(),
                Text = user.Name
            }).ToList(),

            ListingSelectList = listings.Select(listing => new SelectListItem
            {
                Value = listing.ListingId.ToString(),
                Text = listing.Title  
            }).ToList(),
        };
        

        return View(createReservationViewModel);
    }

  
    
    [HttpPost]
    public async Task<IActionResult> CreateReservation(CreateReservationViewModel model)
    {
        try
        {
            
            if (!ModelState.IsValid)
            {
                var user = await _listingDbContext.Users.FindAsync(model.Reservation.UserId);
                var listing = await _listingDbContext.Listings.FindAsync(model.Reservation.ListingId);

                if (user == null || listing == null)
                {
                    _logger.LogError("[ReservationController] Reservation list not found");
                    return NotFound("Reservation list not found");
                }
                

                _listingDbContext.Reservations.Add(model.Reservation);
                await _listingDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Table));
            }

            var users = await _listingDbContext.Users.ToListAsync();
            var listings = await _listingDbContext.Listings.ToListAsync();

            model.UserSelectList = users.Select(user => new SelectListItem
            {
                Value = user.UserId.ToString(),
                Text = user.Name
            }).ToList();

            model.ListingSelectList = listings.Select(listing => new SelectListItem
            {
                Value = listing.ListingId.ToString(),
                Text = listing.Title
            }).ToList();
            
            
            return View(model); 
        }
        catch (Exception e)
        {
            _logger.LogError("[ReservationController] Reservation creation failed for listing {@reservation}, error message: ", e.Message);
            return null;
        }
    }

    [HttpGet]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var reservation = await _listingDbContext.Reservations
            .Include(r => r.Listing)  // Include the related Listing data
            .Include(r => r.User)     // Include the related User data if needed
            .FirstOrDefaultAsync(r => r.ReservationId == id);

        if (reservation == null)
        {
            _logger.LogError("[ReservationController] Reservation not found");
            return NotFound("Reservation not found");
        }

        await CalculateTotalPrice(reservation); // Calculate TotalPrice before passing to the view

        return View(reservation);
    }



    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var reservation = await _listingDbContext.Reservations.FindAsync(id);
        if (reservation == null)
        {
            _logger.LogError("[ReservationController] Reservation list not found");
            return NotFound("Reservation list not found");
        }

        _listingDbContext.Reservations.Remove(reservation);
        await _listingDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Table));
    }
}

