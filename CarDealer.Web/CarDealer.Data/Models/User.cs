namespace CarDealer.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public byte[] Photo { get; set; }
    }
}
