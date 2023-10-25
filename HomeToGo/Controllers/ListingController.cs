using Microsoft.AspNetCore.Mvc;
using HomeToGo.Models;
using HomeToGo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyShop.Controllers;

public class ListingController : Controller
{
    private readonly ListingDbContext _listingDbContext;

    public ListingController(ListingDbContext listingDbContext)
    {
        _listingDbContext = listingDbContext;
    }

    public async Task<IActionResult> Table()
    {
        List<Listing> listings = await _listingDbContext.Listings.ToListAsync();
        var listingListViewModel = new ListingListViewModel(listings, "Table");
        return View(listingListViewModel);
    }

    public async Task<IActionResult> Grid()
    {
        List<Listing> listings = await _listingDbContext.Listings.ToListAsync();
        var listingListViewModel = new ListingListViewModel(listings, "Grid");
        return View(listingListViewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        List<Listing> listings = await _listingDbContext.Listings.ToListAsync();
        var listing = listings.FirstOrDefault(i => i.ListingId == id);
        if (listing == null)
            return NotFound();
        return View(listing);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(Listing listing)
    {
        if (ModelState.IsValid)
        {
            _listingDbContext.Listings.Add(listing);
            await _listingDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Table));
        }

        return View(listing);

    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Update(int id)
    {
        var listing = await _listingDbContext.Listings.FindAsync(id);
        if (listing == null)
        {
            return NotFound();
        }

        return View(listing);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Update(Listing listing)
    {
        if (ModelState.IsValid)
        {
            _listingDbContext.Listings.Update(listing);
            await _listingDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Table));
        }

        return View(listing);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var listing = await _listingDbContext.Listings.FindAsync(id);
        if (listing == null)
        {
            return NotFound();
        }

        return View(listing);
        
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var listing = await _listingDbContext.Listings.FindAsync(id);
        if (listing == null)
        {
            return NotFound();
        }

        _listingDbContext.Listings.Remove(listing); 
        await _listingDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Table));
       
    }


}
// Kommentert til når vi har fikset DB
/* [HttpPost]
 public async Task<IActionResult> Create(Listing listing)
 {
     if (ModelState.IsValid)
     {
         _listingDbContext.Listings.Add(listing);
         await _listingDbContext.SaveChangesAsync();
         return RedirectToAction(nameof(Table));
     }
     return View(listing);
 }
}
    /*public List<Listing> GetListings()
    {
        var listings = new List<Listing>();
        var listing1 = new Listing
        {
            ListingId = 1,
            Title = "Tjuvholem, Penthouse",
            Address = "Osloveien 36",
            Price = 2500,
            Description = "Fin topp leilighet i midten av Oslo, Med utsikt over hele byen",
            ImageUrl = "/Images/Ap1.jpg"

        };
        var listing2 = new Listing
        {
            ListingId = 2,
            Title = "Montebello, Villa",
            Address = "Montebelloveien 7",
            Price = 150,
            Description = "Stor villa i vest-kanten av Oslo, inkluderer en porche med på leie",
            ImageUrl = "/Images/Ap2.jpg"

        };

        var listing3 = new Listing
        {
            ListingId = 3,
            Title = "Pilistredet student hus",
            Address = "Pilistredet 32",
            Price = 500,
            Description = "Student leilighet rett ved OsloMet",
            ImageUrl = "/Images/Ap3.jpg"


        };

        var listing4 = new Listing
        {
            ListingId = 4,
            Title = "Lido beach, House",
            Address = "Lido 1",
            Price = 4500,
            Description = "Stor kyst hus i Lido Beach Mogadishu",
            ImageUrl = "/Images/Ap4.jpg"

        };
        var listing5 = new Listing
        {
            ListingId = 5,
            Title = "Hovseter, Krigere",
            Address = "Hovster 43",
            Price = 10,
            Description = "Trap Bando ved Hovseter T-bane",
            ImageUrl = "/Images/Ap5.jpg"

        };
        var listing6 = new Listing
        {
            ListingId = 6,
            Title = "Bjerke, leiglighet",
            Address = "Bjerkeveien 73",
            Price = 960 ,
            Description = "Fin leiglighet i midten av Bjerke",
            ImageUrl = "/Images/Ap6.jpg"

        };

        var listing7 = new Listing
        {
            ListingId = 7,
            Title = "South of france, Villa",
            Address  = "Vive la Nice 7",
            Price = 3500,
            Description = "Stor villa nice, inkluderer maid som tar vare på deg",
            ImageUrl = "/Images/Ap7.jpg"

        };
        var listing8 = new Listing
        {
            ListingId = 8,
            Title = "Hargeisa, Villa",
            Address  = "chigchiga yar 12",
            Price = 7000,
            Description = "Stor Enebolig i Hargeisa",
            ImageUrl = "/Images/Ap8.jpg"

        };

        listings.Add(listing1);
        listings.Add(listing2);
        listings.Add(listing3);
        listings.Add(listing4);
        listings.Add(listing5);
        listings.Add(listing6);
        listings.Add(listing7);
        listings.Add(listing8);
        return listings;
    }



 */




 