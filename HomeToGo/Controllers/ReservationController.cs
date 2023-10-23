using Microsoft.AspNetCore.Mvc;
using HomeToGo.Models;
using Microsoft.EntityFrameworkCore;
using HomeToGo.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace HomeToGo.Controllers
{
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateReservation()
        {
            var users = await _listingDbContext.Users.ToListAsync();
            var listings = await _listingDbContext.Listings.ToListAsync();

            var reservationViewModel = new ReservationViewModel
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

            return View(reservationViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateReservation(ReservationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _listingDbContext.Users.FindAsync(model.Reservation.UserId);
                    var listing = await _listingDbContext.Listings.FindAsync(model.Reservation.ListingId);

                    if (user == null || listing == null)
                    {
                        return BadRequest("User or Listing not found.");
                    }

                    // Add reservation first
                    _listingDbContext.Reservations.Add(model.Reservation);
                    await _listingDbContext.SaveChangesAsync();

                    // If you have a list of ReservationListings in your ViewModel, add them too
                    if (model.ReservationListings != null)
                    {
                        foreach (var reservationListing in model.ReservationListings)
                        {
                            reservationListing.ReservationId = model.Reservation.ReservationId; // Set the ReservationId to the newly created reservation
                            _listingDbContext.ReservationListings.Add(reservationListing);
                        }
                        await _listingDbContext.SaveChangesAsync();
                    }

                    return RedirectToAction(nameof(Table));
                }

                // Repopulate the dropdown lists before returning to the view
                var users = await _listingDbContext.Users.ToListAsync();
                var listings = await _listingDbContext.Listings.ToListAsync();

                model.UserSelectList = users.Select(user => new SelectListItem
                {
                    Value = user.Id.ToString(),
                    Text = user.UserName
                }).ToList();

                model.ListingSelectList = listings.Select(listing => new SelectListItem
                {
                    Value = listing.ListingId.ToString(),
                    Text = listing.Title
                }).ToList();

                return View(model); // Return to the form with model errors
            }
            catch
            {
                return BadRequest("Reservation creation failed.");
            }
        }
    }
}
