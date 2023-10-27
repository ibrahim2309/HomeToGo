using HomeToGo.Models;
using HomeToGo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using HomeToGo.DAL;
using Microsoft.AspNetCore.Authorization;
using Serilog;


namespace HomeToGo.Controllers;

public class ListingController : Controller
{
<<<<<<< Updated upstream
    private readonly IListingRepository _listingRepository;
    private readonly ILogger<ListingController> _logger;
    
    

    public ListingController(IListingRepository listingRepository, ILogger<ListingController> logger)
=======
    private readonly ListingDbContext _listingDbContext;
    
    /*private readonly ILogger<ListingController>_logger
    
    public ListingController(IListingRepository listingRepository, ILogger<ListingController> logger)
    {
        _listingRepository = listingRepository;
        _logger = logger;
    }
    */
    public ListingController(ListingDbContext listingDbContext)
>>>>>>> Stashed changes
    {
        
        _listingRepository = listingRepository;
        _logger = logger;
    }
    
    public async Task<IActionResult> Table()
    {
<<<<<<< Updated upstream
        
        var listings = await _listingRepository.GetAll();
        if (listings == null)
        {
            _logger.LogError("[ListingController] Listing list not found while executing _listingRepository.GetAll()");
            return NotFound("Listing list not found");
        }
=======
       /* 
        _logger.LogInformation("This is an information message.");
        _logger.LogWarning("This is a warning message.");
        _logger.LogError("This is an error message");
        */
        List<Listing> listings = await _listingDbContext.Listings.ToListAsync();
>>>>>>> Stashed changes
        var listingListViewModel = new ListingListViewModel(listings, "Table");
        return View(listingListViewModel);
    }

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

    [HttpGet]
    //[Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    //[Authorize]
    public async Task<IActionResult> Create(Listing listing)
    {
        if (ModelState.IsValid)
        {
            bool returnOk = await _listingRepository.Create(listing);
            if (returnOk)   
                return RedirectToAction(nameof(Table));
        }
        _logger.LogWarning("[ListingController] Listing creation failed {@listing}", listing);
        return View(listing);

    }
/*
    [HttpPost]
    public async Task<IActionResult> Create(Listing listing)
    {
       if (ModelState.IsValid)
       {
       bool returnOk = await listing_Repository.Create(listing);
       if (returnOk)
       return RedirectToAction(nameof(Table));
       }
       _logger.LogWarning(«[ListingController] Listing creation failed {@list}», list);
       return View(list);
    }
    */  /* dette er Input Validation from the Client Side kode */
    
    [HttpGet]
   // [Authorize]
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

    [HttpPost]
  //  [Authorize]
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

    [HttpGet]
   // [Authorize]
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

    [HttpPost]
    //[Authorize]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var listing = await _listingRepository.GetListingById(id);
        if (listing == null)
        {
<<<<<<< Updated upstream
            _logger.LogError("[ListingController] Listing deletion failed for the ListingId {ListingId:0000}", id);
            return BadRequest("Listing deletion failed");
=======
            return NotFound(); 
>>>>>>> Stashed changes
        }

        await _listingRepository.Delete(id); 
        return RedirectToAction(nameof(Table));
       
    }


}
// Kommentert til når vi har fikset DB
/* [HttpPost]
 public async Task<IActionResult> Create(Listing listing)
 {
     if (ModelState.IsValid)
     {
         _listingRepository.Listings.Add(listing);
         await _listingRepository.SaveChangesAsync();
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




 