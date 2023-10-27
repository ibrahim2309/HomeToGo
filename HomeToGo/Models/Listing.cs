using System.ComponentModel.DataAnnotations;

namespace HomeToGo.Models;

public class Listing
{
   [Microsoft.Build.Framework.Required]
    public int ListingId { get; set; }
    
    [StringLength(50)]
    public string Title { get; set; } = string.Empty; 
    
    [StringLength(50, MinimumLength = 4, ErrorMessage = "The address must be between 4 and 50 characters")]
    public string Address { get; set; } = string.Empty;
    
    [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than 0.")]
    public decimal Price { get; set; }
    
    [StringLength(50)]
    public string? Description { get; set; }
    
    public string? ImageUrl { get; set; }
    
}

// Input validation from the server side er done// 