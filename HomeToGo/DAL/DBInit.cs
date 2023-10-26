
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using HomeToGo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HomeToGo.DAL;

public static class DBInit
{
    public static void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ListingDbContext context = serviceScope.ServiceProvider.GetRequiredService<ListingDbContext>();
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();


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
                    ImageUrl = "/Images/Ap1.jpg"
                },
                new Listing
                {
                    ListingId = 2,
                    Title = "Montebello, Villa",
                    Address = "Montebelloveien 7",
                    Price = 150,
                    Description = "Stor villa i vest-kanten av Oslo, inkluderer en porche med på leie",
                    ImageUrl = "/Images/Ap2.jpg"
                },
   
                new Listing
                {
                    ListingId = 3,
                    Title = "Pilistredet student hus",
                    Address = "Pilistredet 32",
                    Price = 500,
                    Description = "Student leilighet rett ved OsloMet",
                    ImageUrl = "/Images/Ap3.jpg"
                },
                new Listing
                {
                    ListingId = 4,
                    Title = "Lido beach, House",
                    Address = "Lido 1",
                    Price = 4500,
                    Description = "Stor kyst hus i Lido Beach Mogadishu",
                    ImageUrl = "/Images/Ap4.jpg"
                },
                new Listing
                {
                    ListingId = 5,
                    Title = "Hovseter, Krigere",
                    Address = "Hovster 43",
                    Price = 10,
                    Description = "Trap Bando ved Hovseter T-bane",
                    ImageUrl = "/Images/Ap5.jpg"
                },
                new Listing
                {
                    ListingId = 6,
                    Title = "Bjerke, leiglighet",
                    Address = "Bjerkeveien 73",
                    Price = 960,
                    Description = "Fin leiglighet i midten av Bjerke",
                    ImageUrl = "/Images/Ap6.jpg"
                },
                new Listing
                {
                    ListingId = 7,
                    Title = "South of france, Villa",
                    Address = "Vive la Nice 7",
                    Price = 3500,
                    Description = "Stor villa nice, inkluderer maid som tar vare på deg",
                    ImageUrl = "/Images/Ap7.jpg"
                },
   
   
            };
            
            context.AddRange(listings);
            context.SaveChanges();
        }


        if (!context.Users.Any())
        {
            var users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    Name = "Abdi",
                    Email = "hei@live.com",
                    Number = "21231321",
                    Address = "Finnes ikke vei 3",
                }

            };
            context.AddRange(users);
            context.SaveChanges();
        }
        
        
        if (!context.Reservations.Any())
        {
            var reservation1 = new Reservation
            {
                ReservationId = 1,
                UserId = 1,
                ListingId = 1, // Replace with actual Listing Id
                CheckInDate = DateTime.Now.AddDays(1),
                CheckOutDate = DateTime.Now.AddDays(7),
                TotalPrice = 700 // Placeholder value
            };

            var reservation2 = new Reservation
            {
                ReservationId = 2,
                UserId = 1, // Replace with actual User Id
                ListingId = 2, // Replace with actual Listing Id
                CheckInDate = DateTime.Now.AddDays(3),
                CheckOutDate = DateTime.Now.AddDays(10),
                TotalPrice = 1000 // Placeholder value
            };

            context.Reservations.Add(reservation1);
            context.Reservations.Add(reservation2);
            
        }
        
    }
}
