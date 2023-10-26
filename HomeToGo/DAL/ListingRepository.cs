using HomeToGo.DAL;
using Microsoft.EntityFrameworkCore;
using HomeToGo.Models;

namespace HomeToGo.DAL;

public class ListingRepository : IListingRepository
{
    private readonly ListingDbContext _db;

    public ListingRepository(ListingDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Listing>> GetAll()
    {
        return await _db.Listings.ToListAsync();
    }

    public async Task<Listing?> GetListingById(int id)
    {
        return await _db.Listings.FindAsync(id);
    }

    public async Task Create(Listing listing)
    {
        _db.Listings.Add(listing);
        await _db.SaveChangesAsync();
    }

    public async Task Update(Listing listing)
    {
        _db.Listings.Update(listing);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> Delete(int id)
    {
        var listing = await _db.Listings.FindAsync(id);
        if (listing == null)
        {
            return false;
        }

        _db.Listings.Remove(listing);
        await _db.SaveChangesAsync();
        return true;
    }
}