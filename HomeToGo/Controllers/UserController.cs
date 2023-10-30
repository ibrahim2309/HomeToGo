using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Controller to manage user-related actions.
public class UserController : Controller
{
    // Dependency on UserManager to interact with user data.
    private readonly UserManager<IdentityUser> _userManager;

    // Constructor to inject UserManager dependency.
    public UserController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    // Action to display a list of users.
    public async Task<IActionResult> Users()
    {
        // Retrieve all users from the database.
        var users = await _userManager.Users.ToListAsync();

        // Render the user list view with the retrieved users.
        return View("~/Views/Shared/_UserTable.cshtml", users);
    }
}