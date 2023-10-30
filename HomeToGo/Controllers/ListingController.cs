using HomeToGo.Models;
using HomeToGo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using HomeToGo.DAL;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HomeToGo.Controllers;

// Controller for handling listings-related actions
public class ListingController : Controller
{
    // Injecting required services and repositories
    private readonly IListingRepository _listingRepository;
    private readonly ILogger<ListingController> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public ListingController(IListingRepository listingRepository, ILogger<ListingController> logger, UserManager<IdentityUser> userManager)
    {
        _listingRepository = listingRepository ?? throw new ArgumentNullException(nameof(listingRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    // Displays all listings in a table format
    public async Task<IActionResult> Table()
    {
        var listings = await _listingRepository.GetAll();
        if (listings == null)
        {
            _logger.LogError("[ListingController] Listing list not found while executing _listingRepository.GetAll()");
            return NotFound("Listing list not found");
        }
        var listingListViewModel = new ListingListViewModel(listings, "Table");
        return View(listingListViewModel);
    }

    // Displays all listings in a grid format
    public async Task<IActionResult> Grid()
    {
        var listings = await _listingRepository.GetAll();
        if (listings == null)
        {
            _logger.LogError("[ListingController] Listing list not found while executing _listingRepository.GetAll()");
            return NotFound("Listing list not found");
        }
        var listingListViewModel = new ListingListViewModel(listings, "Grid");
        return View(listingListViewModel);
    }

    // Displays details for a specific listing
    public async Task<IActionResult> Details(int id)
    {
        var listing = await _listingRepository.GetListingById(id);
        if (listing == null)
        {
            _logger.LogError("[ListingController] Listing list not found while executing _listingRepository.GetAll()");
            return NotFound("Listing list not found");
        }

        return View(listing);
    }

    // Presents the form for creating a new listing
    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    // Handles the submission of the new listing form
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(Listing listing)
    {
        if (ModelState.IsValid)
        {
            listing.UserId = _userManager.GetUserId(User); // Setting the UserId for the listing
            bool returnOk = await _listingRepository.Create(listing);
            if (returnOk)
                return RedirectToAction(nameof(Table));
        }
        _logger.LogWarning("[ListingController] Listing creation failed {@listing}", listing);
        return View(listing);
    }

    // Presents the form for updating an existing listing
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Update(int id)
    {
        var listing = await _listingRepository.GetListingById(id);
        if (listing == null)
        {
            _logger.LogError("[ListingController] Listing not found when updating the ListingId {ListingId:0000}", id);
            return BadRequest("Listing not found for the ListingId");
        }

        return View(listing);
    }

    // Handles the submission of the update listing form
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Update(Listing listing)
    {
        if (ModelState.IsValid)
        {
            bool returnOk = await _listingRepository.Update(listing);
            if (returnOk)
                return RedirectToAction(nameof(Table));
        }
        _logger.LogWarning("[ListingController] Listing update failed {@listing}", listing);
        return View(listing);
    }

    // Presents the confirmation form for deleting a listing
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var listing = await _listingRepository.GetListingById(id);
        if (listing == null)
        {
            _logger.LogError("[ListingController] Listing not found for the ListingId {ListingId:0000}", id);
            return BadRequest("Listing not found for the ListingId");
        }

        return View(listing);
    }

    // Handles the confirmation of listing deletion
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var listing = await _listingRepository.GetListingById(id);
        if (listing == null)
        {
            _logger.LogError("[ListingController] Listing deletion failed for the ListingId {ListingId:0000}", id);
            return BadRequest("Listing deletion failed");
        }

        await _listingRepository.Delete(id);
        return RedirectToAction(nameof(Table));
    }
}
