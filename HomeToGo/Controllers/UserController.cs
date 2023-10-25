using HomeToGo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeToGo.Controllers;

public class UserController : Controller
{
    private readonly ListingDbContext _listingDbContext;

    public UserController(ListingDbContext listingDbContext)
    {
        _listingDbContext = listingDbContext;
    }

    public async Task<IActionResult> Table()
    {
        List<User> users = await _listingDbContext.Users.ToListAsync();
        return View(users);
    }
}