using Microsoft.AspNetCore.Mvc;
using HomeToGo.Models;

namespace HomeToGo.Controllers;

public class ListingController : Controller
{
    // GET
    public IActionResult Table()
    {
        var listings = GetListings();
        ViewBag.CurrentViewName = "Table";
        return View(listings);

    }

    public IActionResult Grid()
    {
        var listings = GetListings();
        ViewBag.CurrentViewName = "Grid";
        return View(listings);
    }

    public List<Listing> GetListings()
    {
        var listings = new List<Listing>();
        var listing1 = new Listing
        {
            ListingId = 1,
            Title = "Tjuvholem, Penthouse",
            Address = "Osloveien 36",
            Price = 2500,
            Description = "Fin topp leilighet i midten av Oslo, Med utsikt over hele byen",

        };
        var listing2 = new Listing
        {
            ListingId = 2,
            Title = "Montebello, Villa",
            Address = "Montebelloveien 7",
            Price = 150,
            Description = "Stor villa i vest-kanten av Oslo, inkluderer en porche med på leie",

        };
        
        var listing3 = new Listing
        {
            ListingId = 3,
            Title = "Pilistredet student hus",
            Address = "Pilistredet 32",
            Price = 500,
            Description = "Student leilighet rett ved OsloMet",

        };
        
        var listing4 = new Listing
        {
            ListingId = 4,
            Title = "Lido beach, House",
            Address = "Lido 1",
            Price = 4500,
            Description = "Stor kyst hus i Lido Beach Mogadishu",

        };
        var listing5 = new Listing
        {
            ListingId = 5,
            Title = "Hovseter, Krigere",
            Address = "Hovster 43",
            Price = 10,
            Description = "Trap Bando ved Hovseter T-bane",

        };
        var listing6 = new Listing
        {
            ListingId = 6,
            Title = "Bjerke, leiglighet",
            Address = "Bjerkeveien 73",
            Price = 960 ,
            Description = "Fin leiglighet i midten av Bjerke",

        };
        
        var listing7 = new Listing
        {
            ListingId = 7,
            Title = "South of france, Villa",
            Address  = "Vive la Nice 7",
            Price = 3500,
            Description = "Stor villa nice, inkluderer maid som tar vare på deg",

        };
        var listing8 = new Listing
        {
            ListingId = 8,
            Title = "Hargeisa, Villa",
            Address  = "chigchiga yar 12",
            Price = 7000,
            Description = "Stor Enebolig i Hargeisa",

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
    
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
// Kommentert til når vi har fikset DB
/* [HttpPost]
 public async Task<IActionResult> Create(Listing item)
 {
     if (ModelState.IsValid)
     {
         _itemDbContext.Items.Add(item);
         await _itemDbContext.SaveChangesAsync();
         return RedirectToAction(nameof(Table));
     }
     return View(item);
 }
 */


}

 