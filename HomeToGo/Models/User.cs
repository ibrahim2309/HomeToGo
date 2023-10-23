namespace HomeToGo.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public bool IsHost { get; set; }
    }

}