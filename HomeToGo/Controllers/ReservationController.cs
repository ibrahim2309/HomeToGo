using HomeToGo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeToGo.DAL;
using HomeToGo.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HomeToGo.Controllers;

// Controller to handle reservations.
public class ReservationController : Controller
{
    // Logging, database context, and user manager dependencies.
    private readonly ILogger<ReservationController> _logger;
    private readonly ListingDbContext _listingDbContext;
    private readonly UserManager<IdentityUser> _userManager;

    // Constructor to inject dependencies.
    public ReservationController(ListingDbContext listingDbContext, ILogger<ReservationController> logger,
        UserManager<IdentityUser> userManager)
    {
        _listingDbContext = listingDbContext;
        _logger = logger;
        _userManager = userManager;
    }

    // Helper method to calculate total price of a reservation.
    private async Task CalculateTotalPrice(Reservation reservation)
    {
        // Validations
        if (reservation == null) throw new ArgumentNullException(nameof(reservation), "Reservation cannot be null");
        if (reservation.ListingId == null) throw new ArgumentException("Reservation must have a ListingId");

        var listing = await _listingDbContext.Listings.FindAsync(reservation.ListingId);
        if (listing == null) throw new ArgumentException("Invalid ListingId in Reservation");

        // Calculate total price
        TimeSpan stayDuration = reservation.CheckOutDate - reservation.CheckInDate;
        reservation.TotalPrice = listing.Price * stayDuration.Days;
    }

    // Helper method to check if a reservation overlaps with existing ones.
    private async Task<bool> IsReservationOccupied(Reservation newReservation)
    {
        return await _listingDbContext.Reservations.AnyAsync(r => r.ListingId == newReservation.ListingId &&
                                                                  r.ReservationId != newReservation.ReservationId &&
                                                                  ((newReservation.CheckInDate >= r.CheckInDate &&
                                                                    newReservation.CheckInDate < r.CheckOutDate) ||
                                                                   (newReservation.CheckOutDate > r.CheckInDate &&
                                                                    newReservation.CheckOutDate <= r.CheckOutDate) ||
                                                                   (newReservation.CheckInDate <= r.CheckInDate &&
                                                                    newReservation.CheckOutDate >= r.CheckOutDate)));
    }

    // Displays all reservations in a table view.
    public async Task<IActionResult> Table()
    {
        var reservations = await _listingDbContext.Reservations.Include(r => r.Listing).ToListAsync();
        foreach (var reservation in reservations) await CalculateTotalPrice(reservation);

        return View(reservations);
    }

    // GET: Create a reservation.
    [HttpGet]
    public async Task<IActionResult> CreateReservation()
    {
        // Preparing data for dropdowns in the view
        var users = await _listingDbContext.Users.ToListAsync();
        var listings = await _listingDbContext.Listings.ToListAsync();

        var createReservationViewModel = new CreateReservationViewModel
        {
            Reservation = new Reservation(),
            UserSelectList = users.Select(user => new SelectListItem
            {
                Value = user.Id.ToString(),
                Text = user.UserName
            }).ToList(),
            ListingSelectList = listings.Select(listing => new SelectListItem
            {
                Value = listing.ListingId.ToString(),
                Text = listing.Title
            }).ToList(),
        };

        return View(createReservationViewModel);
    }

    // POST: Create a reservation.
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateReservation(CreateReservationViewModel model)
    {
        try
        {
            // Removing unnecessary model validations
            ModelState.Remove("Reservation.UserId");
            ModelState.Remove("Reservation.User");

            if (!ModelState.IsValid)
            {
                var loggedInUser = await _userManager.GetUserAsync(User);
                if (loggedInUser == null) return Unauthorized("No logged-in user found.");
                model.Reservation.UserId = loggedInUser.Id;

                // Checking if reservation dates are available
                if (await IsReservationOccupied(model.Reservation))
                {
                    ModelState.AddModelError(string.Empty, "The reservation is occupied for the selected dates.");
                    _logger.LogError("[ReservationController] The reservation is occupied for the selected dates");
                }
                else
                {
                    // Fetching user and listing, and validating
                    var user = await _listingDbContext.Users.FindAsync(model.Reservation.UserId);
                    var listing = await _listingDbContext.Listings.FindAsync(model.Reservation.ListingId);

                    if (user == null) ModelState.AddModelError("Reservation.UserId", "User not found.");
                    if (listing == null) ModelState.AddModelError("Reservation.ListingId", "Please select a listing.");

                    if (user != null && listing != null)
                    {
                        _listingDbContext.Reservations.Add(model.Reservation);
                        await _listingDbContext.SaveChangesAsync();
                        return RedirectToAction(nameof(Table));
                    }
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError($"[ReservationController] Reservation creation failed for listing {model.Reservation.ListingId}, error message: {e.Message}");
            ModelState.AddModelError(string.Empty, "Error creating reservation.");
        }

        // Load lists for dropdowns in case of error
        model.UserSelectList = await _listingDbContext.Users.Select(user => new SelectListItem
        {
            Value = user.Id.ToString(),
            Text = user.UserName
        }).ToListAsync();

        model.ListingSelectList = await _listingDbContext.Listings.Select(listing => new SelectListItem
        {
            Value = listing.ListingId.ToString(),
            Text = listing.Title
        }).ToListAsync();

        return View(model);
    }

    // GET: Delete a reservation.
    [HttpGet]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var reservation = await _listingDbContext.Reservations.Include(r => r.Listing).Include(r => r.User).FirstOrDefaultAsync(r => r.ReservationId == id);
        if (reservation == null)
        {
            _logger.LogError("[ReservationController] Reservation not found");
            return NotFound("Reservation not found");
        }

        await CalculateTotalPrice(reservation); // Calculate TotalPrice before passing to the view
        return View(reservation);
    }

    // POST: Confirm deletion of a reservation.
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var reservation = await _listingDbContext.Reservations.FindAsync(id);
        if (reservation == null)
        {
            _logger.LogError("[ReservationController] Reservation not found");
            return NotFound("Reservation not found");
        }

        _listingDbContext.Reservations.Remove(reservation);
        await _listingDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Table));
    }
}
