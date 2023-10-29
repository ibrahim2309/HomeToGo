using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;

namespace HomeToGo.Models
{
    public class Listing
    {
        public int ListingId { get; set; }
        public string? UserId { get; set; }
        
        public virtual IdentityUser? User { get; set; }
        
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
}