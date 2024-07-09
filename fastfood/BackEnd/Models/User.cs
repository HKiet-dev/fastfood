using Microsoft.AspNetCore.Identity;

namespace BackEnd.Models
{
    public class User : IdentityUser
    {
        public int Gender { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public List<CartDetail>? CartDetails { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
