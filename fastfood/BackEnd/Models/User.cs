using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class User : IdentityUser
    {
        [StringLength(50, MinimumLength =3, ErrorMessage ="Độ dài của tên phải từ 3 đến 50 ký tự")]
        [RegularExpression("^[a-zA-Z 0-9_]+$", ErrorMessage = "Tên tài khoản phải là ký tự không dấu hoặc số")]
        public string Name { get; set; }
       
        public int Gender { get; set; }
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Độ dài địa chỉ phải từ 5 đến 250 ký tự")]
        public string Address { get; set; }
        public string Avatar { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public List<CartDetail>? CartDetails { get; set; }
        public List<Order>? Orders { get; set; }
        public Cart Cart { get; set; }
    }
}
