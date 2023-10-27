namespace HomeToGo.Models;

public class Listing
{
    public int ListingId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    
    public string? ImageUrl { get; set; }
    
}