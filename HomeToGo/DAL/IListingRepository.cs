using HomeToGo.Models;

namespace HomeToGo.DAL;

public interface IListingRepository
{
    Task<IEnumerable<Listing>> GetAll();
    Task<Listing?> GetListingById(int id);
    Task Create(Listing listing);
    Task Update(Listing listing);
    Task<bool> Delete(int id);
}