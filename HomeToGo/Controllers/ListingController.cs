using Microsoft.AspNetCore.Mvc;
using HomeToGo.Models;

namespace HomeToGo.Controllers;

public class ListingController : Controller
{
    
    // GET
    public IActionResult Table()
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
            Price = 2500,
            Description = "Stor villa i vest-kanten av Oslo, inkluderer en porche med på leie",

        };
        
        listings.Add(listing1);
        listings.Add(listing2);
        ViewBag.CurrentViewName = "Listings in Home To Go";
        return View(listings);

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