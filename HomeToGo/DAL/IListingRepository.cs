using HomeToGo.Models;

namespace HomeToGo.DAL;

//Interface for ListingRepository
public interface IListingRepository
{
    Task<IEnumerable<Listing>> GetAll();
    Task<Listing?> GetListingById(int id);
    Task<bool> Create(Listing listing);
    Task<bool> Update(Listing listing);
    Task<bool> Delete(int id);
}