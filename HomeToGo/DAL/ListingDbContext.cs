using System;
using HomeToGo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace HomeToGo.DAL;

public class ListingDbContext : IdentityDbContext
{
    public ListingDbContext(DbContextOptions<ListingDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    public DbSet<Listing> Listings { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<IdentityUser> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
    
  
}