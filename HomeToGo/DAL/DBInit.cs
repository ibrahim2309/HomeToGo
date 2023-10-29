
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using HomeToGo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;


namespace HomeToGo.DAL;

public static class DBInit
{
    public static void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ListingDbContext context = serviceScope.ServiceProvider.GetRequiredService<ListingDbContext>();
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        var oleUser = new IdentityUser { UserName = "Ole Hansen", Email = "ole.hansen@email.no", PasswordHash = "HashedPassword1", EmailConfirmed = true }; 
        var kariUser = new IdentityUser { UserName = "Kari Johansen", Email = "kari.johansen@email.no", PasswordHash = "HashedPassword2", EmailConfirmed = true }; 
        var larsUser = new IdentityUser { UserName = "Lars Olsen", Email = "lars.olsen@email.no", PasswordHash = "HashedPassword3", EmailConfirmed = true }; 

// Check if users already exist to avoid duplicate seeding
        if (!context.Users.Any(u => u.UserName == oleUser.UserName))
        {
            context.Users.Add(oleUser);
        }
        if (!context.Users.Any(u => u.UserName == kariUser.UserName))
        {
            context.Users.Add(kariUser);
        }
        if (!context.Users.Any(u => u.UserName == larsUser.UserName))
        {
            context.Users.Add(larsUser);
        }

        context.SaveChanges();

        if (!context.Listings.Any())
        {
            var listings = new List<Listing>
            {
                new Listing()
                {
                    ListingId = 1,
                    Title = "Tjuvholem, Penthouse",
                    Address = "Osloveien 36",
                    Price = 2500,
                    Description = "Fin topp leilighet i midten av Oslo, Med utsikt over hele byen",
                    UserId = oleUser.Id,
                    ImageUrl = "/Images/Ap1.jpg"
                },
                new Listing
                {
                    ListingId = 2,
                    Title = "Montebello, Villa",
                    Address = "Montebelloveien 7",
                    Price = 150,
                    Description = "Stor villa i vest-kanten av Oslo, inkluderer en porche med på leie",
                    UserId = kariUser.Id,
                    ImageUrl = "/Images/Ap2.jpg"
                },
   
                new Listing
                {
                    ListingId = 3,
                    Title = "Pilistredet student hus",
                    Address = "Pilistredet 32",
                    Price = 500,
                    Description = "Student leilighet rett ved OsloMet",
                    UserId = oleUser.Id,
                    ImageUrl = "/Images/Ap3.jpg"
                },
                new Listing
                {
                    ListingId = 4,
                    Title = "Lido beach, House",
                    Address = "Lido 1",
                    Price = 4500,
                    Description = "Stor kyst hus i Lido Beach Mogadishu",
                    UserId = oleUser.Id,
                    ImageUrl = "/Images/Ap4.jpg"
                },
                new Listing
                {
                    ListingId = 5,
                    Title = "Hovseter, Krigere",
                    Address = "Hovster 43",
                    Price = 10,
                    Description = "Trap Bando ved Hovseter T-bane",
                    UserId = oleUser.Id,
                    ImageUrl = "/Images/Ap5.jpg"
                },
                new Listing
                {
                    ListingId = 6,
                    Title = "Bjerke, leiglighet",
                    Address = "Bjerkeveien 73",
                    Price = 960,
                    Description = "Fin leiglighet i midten av Bjerke",
                    UserId = larsUser.Id,
                    ImageUrl = "/Images/Ap6.jpg"
                },
                new Listing
                {
                    ListingId = 7,
                    Title = "South of france, Villa",
                    Address = "Vive la Nice 7",
                    Price = 3500,
                    Description = "Stor villa nice, inkluderer maid som tar vare på deg",
                    UserId = kariUser.Id,
                    ImageUrl = "/Images/Ap7.jpg"
                },
   
   
            };
            
            context.AddRange(listings);
            context.SaveChanges();
        }
        
        if (!context.Reservations.Any())
        {
            var reservations = new List<Reservation> // Note the change here from 'reservation' to 'reservations'
            {
                new Reservation()
                {
                    ReservationId = 1,
                    UserId = oleUser.Id,
                    ListingId = 1, 
                    CheckInDate = DateTime.Now.AddDays(1),
                    CheckOutDate = DateTime.Now.AddDays(7),
                    TotalPrice = 700 
                },
                new Reservation()
                {
                    ReservationId = 2,
                    UserId = kariUser.Id,
                    ListingId = 1, 
                    CheckInDate = DateTime.Now.AddDays(1),
                    CheckOutDate = DateTime.Now.AddDays(7),
                    TotalPrice = 700 
                },
                new Reservation()
                {
                    ReservationId = 3,
                    UserId = larsUser.Id,
                    ListingId = 1, 
                    CheckInDate = DateTime.Now.AddDays(1),
                    CheckOutDate = DateTime.Now.AddDays(7),
                    TotalPrice = 700 
                }
            };  // <- The missing semicolon and closing brace are added here.

            context.AddRange(reservations);
            context.SaveChanges();
        }

    }
}
