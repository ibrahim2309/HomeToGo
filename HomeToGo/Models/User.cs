using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace HomeToGo.Models
{
    public class User
    {
<<<<<<< Updated upstream
        public int UserId { get; set; }
=======
        [Required]
        public int UserID { get; set; }
        
       [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-] { 2,20}"  ,ErrorMessage = "The Name must be letters and between 2 to 20 characters" )]
       
       [Display(Name = "User name")]
>>>>>>> Stashed changes
        public string Name { get; set; } = string.Empty;
        
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; } = string.Empty;
        
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Number { get; set; } = string.Empty;
        
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The address must be between 4 and 50 Charecters")]
        public string Address { get; set; } = string.Empty;
        
<<<<<<< Updated upstream
        public virtual List<Reservation> Reservations { get; set; }
=======
        [Display(Name = "IsHost")]
        public bool IsHost { get; set; } = false;
>>>>>>> Stashed changes
    }
}

// Input Validation from the server er done//