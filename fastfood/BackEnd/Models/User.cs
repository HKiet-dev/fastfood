using BackEnd.Repository.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
namespace BackEnd.Models
{
    public class User : IdentityUser
    {
        [StringLength(50, MinimumLength =3, ErrorMessage ="Độ dài của tên phải từ 3 đến 50 ký tự")]
        [RegularExpression("^[a-zA-Z 0-9_]+$", ErrorMessage = "Tên tài khoản phải là ký tự không dấu hoặc số")]
        public string Name { get; set; }
        public int Gender { get; set; }
        [AddressValidation]
        public string? Address { get; set; }
        [AllowNull]
        public string? Avatar { get; set; }
        [Required]
        [EnumDataType(typeof(UserStatus))]
        public UserStatus Status { get; set; } = UserStatus.Active;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public List<CartDetail>? CartDetails { get; set; }
        public List<Order>? Orders { get; set; }
    }

    public enum GenderType
    {
        Female,
        Male,
    }

    public enum UserStatus
    {
        Active,
        Inactive,
        Delete,
        Block
    }

}
