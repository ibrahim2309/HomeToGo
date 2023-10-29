using HomeToGo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeToGo.DAL;
using HomeToGo.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace HomeToGo.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly ListingDbContext _listingDbContext;

<<<<<<< Updated upstream
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
        
        TimeSpan stayDuration = reservation.CheckOutDate - reservation.CheckInDate;
        reservation.TotalPrice = listing.Price * stayDuration.Days;
    }
    
    private async Task<bool> IsReservationOccupied(Reservation newReservation)
    {
        return await _listingDbContext.Reservations
            .AnyAsync(r => r.ListingId == newReservation.ListingId &&
                           r.ReservationId != newReservation.ReservationId &&   // to exclude the reservation itself if it's an update
                           ((newReservation.CheckInDate >= r.CheckInDate && newReservation.CheckInDate < r.CheckOutDate) ||
                            (newReservation.CheckOutDate > r.CheckInDate && newReservation.CheckOutDate <= r.CheckOutDate) ||
                            (newReservation.CheckInDate <= r.CheckInDate && newReservation.CheckOutDate >= r.CheckOutDate)));
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
=======
        private readonly UserManager<IdentityUser> _userManager;

        public ReservationController(ListingDbContext listingDbContext, ILogger<ReservationController> logger, UserManager<IdentityUser> userManager)
        {
            _listingDbContext = listingDbContext;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Table()
        {
            List<Reservation> reservations = await _listingDbContext.Reservations
                .Include(r => r.Listing)
                .ToListAsync();
            return View(reservations);
        }

        public static decimal CalculateTotalPrice(Listing listing, Reservation reservation)
>>>>>>> Stashed changes
        {
            if (listing == null || reservation == null)
            {
<<<<<<< Updated upstream
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
                if (await IsReservationOccupied(model.Reservation))
                {
                    ModelState.AddModelError(string.Empty, "The reservation is occupied for the selected dates.");
                    _logger.LogError("[ReservationController] The reservation is occupied for the selected dates");
                }
                else
                {
                    var user = await _listingDbContext.Users.FindAsync(model.Reservation.UserId);
                    var listing = await _listingDbContext.Listings.FindAsync(model.Reservation.ListingId);

                    if (user == null || listing == null)
                    {
                        _logger.LogError("[ReservationController] User or Listing not found");
                        return NotFound("User or Listing not found");
                    }

                    _listingDbContext.Reservations.Add(model.Reservation);
                    await _listingDbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Table));
                }
=======
                throw new ArgumentNullException("Listing or Reservation cannot be null");
>>>>>>> Stashed changes
            }

            if (reservation.CheckOutDate <= reservation.CheckInDate)
            {
                throw new ArgumentException("CheckOutDate must be greater than CheckInDate");
            }

            TimeSpan stayDuration = reservation.CheckOutDate - reservation.CheckInDate;
            decimal totalPrice = listing.Price * stayDuration.Days;

            return totalPrice;
        }

        [HttpGet]
        public async Task<IActionResult> CreateReservation()
        {
            var listings = await _listingDbContext.Listings.ToListAsync();

            var createReservationViewModel = new CreateReservationViewModel
            {
                Reservation = new Reservation(),

<<<<<<< Updated upstream
            model.ListingSelectList = listings.Select(listing => new SelectListItem
            {
                Value = listing.ListingId.ToString(),
                Text = listing.Title
            }).ToList();

            return View(model);
=======
                ListingSelectList = listings.Select(listing => new SelectListItem
                {
                    Value = listing.ListingId.ToString(),
                    Text = listing.Title
                }).ToList(),
            };

            return View(createReservationViewModel);
>>>>>>> Stashed changes
        }

        [HttpPost]
        [Authorize]  
        public async Task<IActionResult> CreateReservation(CreateReservationViewModel model)
        {
<<<<<<< Updated upstream
            _logger.LogError("[ReservationController] Reservation creation failed: {errorMessage}", e.Message);
            return View("Error");
=======
            try
            {
                if (!ModelState.IsValid)
                {
                    var loggedInUser = await _userManager.GetUserAsync(User);
                    if (loggedInUser == null)
                    {
                        return Unauthorized("No logged-in user found.");
                    }
    
                    model.Reservation.UserId = loggedInUser.Id;

            
                    // Calculate the total price
                    var listing = await _listingDbContext.Listings.FindAsync(model.Reservation.ListingId);
                    if (listing == null)
                    {
                        _logger.LogError("[ReservationController] Reservation list not found while executing _listingRepository.GetAll()");
                        return NotFound("Reservation list not found");
                    }
            
                    model.Reservation.TotalPrice = (int)CalculateTotalPrice(listing, model.Reservation);

                    
                    _listingDbContext.Reservations.Add(model.Reservation);
                    await _listingDbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Table));
                }

                
                var listings = await _listingDbContext.Listings.ToListAsync();
                model.ListingSelectList = listings.Select(listing => new SelectListItem
                {
                    Value = listing.ListingId.ToString(),
                    Text = listing.Title
                }).ToList();

                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError($"[ReservationController] Reservation creation failed for listing {model.Reservation.ListingId}, error message: {e.Message}");
                return BadRequest("Error creating reservation.");
            }
>>>>>>> Stashed changes
        }

    }
<<<<<<< Updated upstream


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
=======
>>>>>>> Stashed changes
}
