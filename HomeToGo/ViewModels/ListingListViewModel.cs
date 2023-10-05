using HomeToGo.Models;


namespace HomeToGo.ViewModels;

public class ListingListViewModel
{
    public IEnumerable<Listing> Listings;
    public string? CurrentViewName;

    public ListingListViewModel(IEnumerable<Listing> listings, string? currentViewName)
    {
        Listings = listings;
        CurrentViewName = currentViewName;
    }

}