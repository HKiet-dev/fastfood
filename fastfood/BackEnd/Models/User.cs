using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class User : IdentityUser
    {
        public int Gender { get; set; }
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Độ dài địa chỉ phải từ 5 đến 250 ký tự")]
        public string Address { get; set; }
        public string Avatar { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public List<CartDetail>? CartDetails { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
