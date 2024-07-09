using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class User : IdentityUser
    {
        [StringLength(50)]
        public string Name { get; set; }
        public int Gender { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        public string Avatar { get; set; }
        public List<CartDetail>? CartDetails { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
