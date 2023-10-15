using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeToGo.Models;

public class ListingDbContext : IdentityDbContext
{
    public ListingDbContext(DbContextOptions<ListingDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    public DbSet<Listing> Listings { get; set; }
}